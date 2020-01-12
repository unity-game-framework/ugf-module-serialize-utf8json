using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Module.Serialize.Runtime;
using UGF.Module.Serialize.Utf8Json.Runtime.TypeRegisters;
using UGF.Utf8Json.Runtime;
using UGF.Utf8Json.Runtime.Resolver;
using UnityEngine;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Serialize/SerializeUtf8JsonModuleInfo", order = 2000)]
    public class SerializeUtf8JsonModuleInfoAsset : ApplicationModuleInfoAsset<ISerializeUtf8JsonModule>
    {
        [SerializeField] private string m_bytesSerializerName = SerializerUtf8JsonUtility.SerializerBytesName;
        [SerializeField] private string m_textCompactSerializerName = SerializerUtf8JsonUtility.SerializerTextCompactName;
        [SerializeField] private string m_textReadableSerializerName = SerializerUtf8JsonUtility.SerializerTextReadableName;
        [SerializeField] private List<Utf8JsonResolverAsset> m_resolverAssets = new List<Utf8JsonResolverAsset>();
        [SerializeField] private List<SerializeUtf8JsonTypeRegisterAsset> m_typeRegisterAssets = new List<SerializeUtf8JsonTypeRegisterAsset>();

        public string BytesSerializerName { get { return m_bytesSerializerName; } set { m_bytesSerializerName = value; } }
        public string TextCompactSerializerName { get { return m_textCompactSerializerName; } set { m_textCompactSerializerName = value; } }
        public string TextReadableSerializerName { get { return m_textReadableSerializerName; } set { m_textReadableSerializerName = value; } }
        public List<Utf8JsonResolverAsset> ResolverAssets { get { return m_resolverAssets; } }
        public List<SerializeUtf8JsonTypeRegisterAsset> TypeRegisterAssets { get { return m_typeRegisterAssets; } }

        public ISerializeUtf8JsonModuleDescription GetDescription()
        {
            var description = new SerializeUtf8JsonModuleDescription
            {
                BytesSerializerName = m_bytesSerializerName,
                TextCompactSerializerName = m_textCompactSerializerName,
                TextReadableSerializerName = m_textReadableSerializerName
            };

            for (int i = 0; i < m_resolverAssets.Count; i++)
            {
                Utf8JsonResolverAsset asset = m_resolverAssets[i];
                IJsonFormatterResolver resolver = asset.GetResolver();

                description.Resolvers.Add(resolver);
            }

            for (int i = 0; i < m_typeRegisterAssets.Count; i++)
            {
                SerializeUtf8JsonTypeRegisterAsset asset = m_typeRegisterAssets[i];
                ISerializeUtf8JsonTypeRegister typeRegister = asset.GetTypeRegister();

                description.TypeRegisters.Add(typeRegister);
            }

            return description;
        }

        protected override IApplicationModule OnBuild(IApplication application)
        {
            var serializeModule = application.GetModule<ISerializeModule>();
            ISerializeUtf8JsonModuleDescription description = GetDescription();
            Utf8JsonFormatterResolver resolver = Utf8JsonUtility.CreateDefaultResolver();

            return new SerializeUtf8JsonModule(serializeModule, description, resolver);
        }
    }
}
