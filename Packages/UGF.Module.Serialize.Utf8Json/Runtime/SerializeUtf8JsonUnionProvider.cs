using System;
using System.Collections.Generic;
using UGF.Utf8Json.Runtime;
using UGF.Utf8Json.Runtime.Formatters.Union;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public class SerializeUtf8JsonUnionProvider : ISerializeUtf8JsonUnionProvider
    {
        public IUtf8JsonFormatterResolver Resolver { get; }
        public IUnionSerializer UnionSerializer { get; }

        private readonly Dictionary<Type, HashSet<Type>> m_unionTypes = new Dictionary<Type, HashSet<Type>>();
        private readonly Dictionary<Type, Type> m_targetTypeToUnionType = new Dictionary<Type, Type>();

        public SerializeUtf8JsonUnionProvider(IUtf8JsonFormatterResolver resolver, IUnionSerializer unionSerializer)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            UnionSerializer = unionSerializer ?? throw new ArgumentNullException(nameof(unionSerializer));
        }

        public void Add<TUnionType, TTargetType>(string typeIdentifier) where TTargetType : TUnionType
        {
            Type unionType = typeof(TUnionType);
            Type targetType = typeof(TTargetType);

            if (string.IsNullOrEmpty(typeIdentifier)) throw new ArgumentException("Value cannot be null or empty.", nameof(typeIdentifier));
            if (m_targetTypeToUnionType.ContainsKey(targetType)) throw new ArgumentException($"The specified target type already exists: '{targetType}'.", nameof(targetType));

            IUnionFormatter<TUnionType> unionFormatter;

            if (Resolver.TryGetFormatter(unionType, out IJsonFormatter value))
            {
                if (value is IUnionFormatter<TUnionType> cast)
                {
                    unionFormatter = cast;
                }
                else
                {
                    throw new ArgumentException($"The found formatter has unexpected union type: '{value}', unionType:'{unionType}'", nameof(targetType));
                }
            }
            else
            {
                var formatter = new UnionFormatter<TUnionType>(UnionSerializer);

                unionFormatter = formatter;

                Resolver.AddFormatter(unionType, formatter);
            }

            unionFormatter.AddFormatter<TTargetType>(typeIdentifier);

            Add(unionType, targetType);
        }

        public void Add(Type unionType, Type targetType, string typeIdentifier, IJsonFormatter formatter)
        {
            if (unionType == null) throw new ArgumentNullException(nameof(unionType));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (string.IsNullOrEmpty(typeIdentifier)) throw new ArgumentException("Value cannot be null or empty.", nameof(typeIdentifier));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (m_targetTypeToUnionType.ContainsKey(targetType)) throw new ArgumentException($"The specified target type already exists: '{targetType}'.", nameof(targetType));

            if (!(Resolver.TryGetFormatter(unionType, out IJsonFormatter value) && value is IUnionFormatter unionFormatter))
            {
                throw new ArgumentException($"The union formatter for the specified union type not found: '{unionType}'.");
            }

            unionFormatter.AddFormatter(targetType, typeIdentifier, formatter);

            Add(unionType, targetType);
        }

        public void Add(Type unionType, Type targetType)
        {
            if (unionType == null) throw new ArgumentNullException(nameof(unionType));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (m_targetTypeToUnionType.ContainsKey(targetType)) throw new ArgumentException($"The specified target type already exists: '{targetType}'.", nameof(targetType));

            if (!m_unionTypes.TryGetValue(unionType, out HashSet<Type> targetTypes))
            {
                targetTypes = new HashSet<Type>();

                m_unionTypes.Add(unionType, targetTypes);
            }

            targetTypes.Add(targetType);
            m_targetTypeToUnionType.Add(targetType, unionType);
        }

        public void Remove(Type unionType, Type targetType)
        {
            if (unionType == null) throw new ArgumentNullException(nameof(unionType));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (m_unionTypes.TryGetValue(unionType, out HashSet<Type> targetTypes))
            {
                if (targetTypes.Remove(targetType) && targetTypes.Count == 0)
                {
                    m_unionTypes.Remove(unionType);
                }
            }

            if (m_targetTypeToUnionType.Remove(targetType))
            {
                RemoveFormatter(unionType, targetType);
            }
        }

        public void Remove(Type unionType)
        {
            if (unionType == null) throw new ArgumentNullException(nameof(unionType));

            if (m_unionTypes.TryGetValue(unionType, out HashSet<Type> targetTypes))
            {
                foreach (Type targetType in targetTypes)
                {
                    if (m_targetTypeToUnionType.Remove(targetType))
                    {
                        RemoveFormatter(unionType, targetType);
                    }
                }

                m_unionTypes.Remove(unionType);
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<Type, HashSet<Type>> pair in m_unionTypes)
            {
                Type unionType = pair.Key;
                HashSet<Type> targetTypes = pair.Value;

                foreach (Type targetType in targetTypes)
                {
                    if (m_targetTypeToUnionType.ContainsKey(targetType))
                    {
                        RemoveFormatter(unionType, targetType);
                    }
                }
            }

            m_unionTypes.Clear();
            m_targetTypeToUnionType.Clear();
        }

        public Type GetUnionType(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (!m_targetTypeToUnionType.TryGetValue(targetType, out Type unionType))
            {
                throw new ArgumentException($"The union type not found for the specified target type: '{targetType}'.");
            }

            return unionType;
        }

        public bool TryGetUnionType(Type targetType, out Type unionType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return m_targetTypeToUnionType.TryGetValue(targetType, out unionType);
        }

        public Dictionary<Type, Type>.Enumerator GetEnumerator()
        {
            return m_targetTypeToUnionType.GetEnumerator();
        }

        private void RemoveFormatter(Type unionType, Type targetType)
        {
            if (Resolver.TryGetFormatter(unionType, out IJsonFormatter value) && value is IUnionFormatter unionFormatter)
            {
                unionFormatter.RemoveFormatter(targetType);

                if (unionFormatter.FormattersCount == 0)
                {
                    Resolver.RemoveFormatter(unionType);
                }
            }
        }
    }
}
