using System;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.Module.Serialize.Runtime;
using UGF.Module.Serialize.Utf8Json.Runtime.TypeRegisters;
using UGF.Utf8Json.Runtime;
using UGF.Utf8Json.Runtime.Formatters.Union;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public class SerializeUtf8JsonModule : ApplicationModuleBase, ISerializeUtf8JsonModule
    {
        public ISerializeModule SerializeModule { get; }
        public ISerializeUtf8JsonModuleDescription Description { get; }
        public IUtf8JsonFormatterResolver Resolver { get { return m_resolver; } }
        public ISerializeUtf8JsonUnionProvider UnionProvider { get { return m_unionProvider; } }

        private readonly Utf8JsonFormatterResolver m_resolver = Utf8JsonUtility.CreateDefaultResolver();
        private readonly UnionSerializer m_unionSerializer = new UnionSerializer();
        private readonly SerializeUtf8JsonUnionProvider m_unionProvider;
        private readonly SerializerUtf8JsonBytes m_serializerBytes;
        private readonly SerializerUtf8Json m_serializerTextCompact;
        private readonly SerializerUtf8Json m_serializerTextReadable;

        public SerializeUtf8JsonModule(ISerializeModule serializeModule, ISerializeUtf8JsonModuleDescription description)
        {
            SerializeModule = serializeModule ?? throw new ArgumentNullException(nameof(serializeModule));
            Description = description ?? throw new ArgumentNullException(nameof(description));

            m_unionProvider = new SerializeUtf8JsonUnionProvider(m_resolver, m_unionSerializer);
            m_serializerBytes = new SerializerUtf8JsonBytes(m_resolver, m_unionProvider);
            m_serializerTextCompact = new SerializerUtf8Json(m_resolver, m_unionProvider, false);
            m_serializerTextReadable = new SerializerUtf8Json(m_resolver, m_unionProvider, true);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            string bytesName = Description.BytesSerializerName;
            string compactName = Description.TextCompactSerializerName;
            string readableName = Description.TextReadableSerializerName;

            SerializeModule.Provider.Add(bytesName, m_serializerBytes);
            SerializeModule.Provider.Add(compactName, m_serializerTextCompact);
            SerializeModule.Provider.Add(readableName, m_serializerTextReadable);

            for (int i = 0; i < Description.Resolvers.Count; i++)
            {
                IJsonFormatterResolver resolver = Description.Resolvers[i];

                Resolver.AddResolver(resolver);
            }

            for (int i = 0; i < Description.TypeRegisters.Count; i++)
            {
                ISerializeUtf8JsonTypeRegister typeRegister = Description.TypeRegisters[i];

                typeRegister.Register(m_resolver, m_unionProvider);
            }

            Log.Debug($"SerializeUtf8JsonModule: serializers: '{bytesName}', '{compactName}', '{readableName}'.");
            Log.Debug($"SerializeUtf8JsonModule: resolvers:'{Description.Resolvers.Count.ToString()}'.");
            Log.Debug($"SerializeUtf8JsonModule: typeRegisters:'{Description.TypeRegisters.Count.ToString()}'.");
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            for (int i = 0; i < Description.TypeRegisters.Count; i++)
            {
                ISerializeUtf8JsonTypeRegister typeRegister = Description.TypeRegisters[i];

                typeRegister.UnRegister(m_resolver, m_unionProvider);
            }

            for (int i = 0; i < Description.Resolvers.Count; i++)
            {
                IJsonFormatterResolver resolver = Description.Resolvers[i];

                Resolver.RemoveResolver(resolver);
            }

            SerializeModule.Provider.Remove<byte[]>(Description.BytesSerializerName);
            SerializeModule.Provider.Remove<string>(Description.TextCompactSerializerName);
            SerializeModule.Provider.Remove<string>(Description.TextReadableSerializerName);
        }
    }
}
