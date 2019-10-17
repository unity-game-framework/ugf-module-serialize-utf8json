using System;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public static class SerializeUtf8JsonUnionProviderExtensions
    {
        public static void Add<TUnionType, TTargetType>(this ISerializeUtf8JsonUnionProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            provider.Add(typeof(TUnionType), typeof(TTargetType));
        }

        public static void Remove<TUnionType, TTargetType>(this ISerializeUtf8JsonUnionProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            provider.Remove(typeof(TUnionType), typeof(TTargetType));
        }

        public static void Remove<TUnionType>(this ISerializeUtf8JsonUnionProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            provider.Remove(typeof(TUnionType));
        }

        public static Type GetUnionType<TTargetType>(this ISerializeUtf8JsonUnionProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            return provider.GetUnionType(typeof(TTargetType));
        }

        public static bool TryGetUnionType<TTargetType>(this ISerializeUtf8JsonUnionProvider provider, out Type unionType)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            return provider.TryGetUnionType(typeof(TTargetType), out unionType);
        }
    }
}
