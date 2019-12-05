using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Module.Runtime;
using UGF.Module.Serialize.Runtime;
using UGF.Utf8Json.Runtime.Resolver;
using UnityEngine;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Serialize/SerializeUtf8JsonModuleBuilder", order = 2000)]
    public class SerializeUtf8JsonModuleBuilderAsset : ModuleBuilderAsset<ISerializeUtf8JsonModule>
    {
        [SerializeField] private string m_bytesSerializerName = SerializerUtf8JsonUtility.SerializerBytesName;
        [SerializeField] private string m_textCompactSerializerName = SerializerUtf8JsonUtility.SerializerTextCompactName;
        [SerializeField] private string m_textReadableSerializerName = SerializerUtf8JsonUtility.SerializerTextReadableName;
        [SerializeField] private List<Utf8JsonResolverAsset> m_resolverAssets = new List<Utf8JsonResolverAsset>();

        public string BytesSerializerName { get { return m_bytesSerializerName; } set { m_bytesSerializerName = value; } }
        public string TextCompactSerializerName { get { return m_textCompactSerializerName; } set { m_textCompactSerializerName = value; } }
        public string TextReadableSerializerName { get { return m_textReadableSerializerName; } set { m_textReadableSerializerName = value; } }
        public List<Utf8JsonResolverAsset> ResolverAssets { get { return m_resolverAssets; } set { m_resolverAssets = value; } }

        protected override IApplicationModule OnBuild(IApplication application, IModuleBuildArguments<object> arguments)
        {
            var serializeModule = application.GetModule<ISerializeModule>();
            SerializeUtf8JsonModuleDescription description = GetDescription();

            return new SerializeUtf8JsonModule(serializeModule, description);
        }

        private SerializeUtf8JsonModuleDescription GetDescription()
        {
            var description = new SerializeUtf8JsonModuleDescription
            {
                BytesSerializerName = m_bytesSerializerName,
                TextCompactSerializerName = m_textCompactSerializerName,
                TextReadableSerializerName = m_textReadableSerializerName
            };

            for (int i = 0; i < m_resolverAssets.Count; i++)
            {
                Utf8JsonResolverAsset resolverAsset = m_resolverAssets[i];
                IJsonFormatterResolver resolver = resolverAsset.GetResolver();

                description.Resolvers.Add(resolver);
            }

            return description;
        }
    }
}
