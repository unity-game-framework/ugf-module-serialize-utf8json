using System;
using System.Collections.Generic;
using UGF.Utf8Json.Runtime.Resolver;
using UnityEngine;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    [Serializable]
    public class SerializeUtf8JsonModuleDescription : ISerializeUtf8JsonModuleDescription
    {
        [SerializeField] private string m_bytesSerializerName = SerializerUtf8JsonUtility.SerializerBytesName;
        [SerializeField] private string m_textCompactSerializerName = SerializerUtf8JsonUtility.SerializerTextCompactName;
        [SerializeField] private string m_textReadableSerializerName = SerializerUtf8JsonUtility.SerializerTextReadableName;
        [SerializeField] private List<Utf8JsonResolverAsset> m_resolvers = new List<Utf8JsonResolverAsset>();

        public string BytesSerializerName { get { return m_bytesSerializerName; } set { m_bytesSerializerName = value; } }
        public string TextCompactSerializerName { get { return m_textCompactSerializerName; } set { m_textCompactSerializerName = value; } }
        public string TextReadableSerializerName { get { return m_textReadableSerializerName; } set { m_textReadableSerializerName = value; } }
        public List<Utf8JsonResolverAsset> Resolvers { get { return m_resolvers; } }

        IReadOnlyList<Utf8JsonResolverAsset> ISerializeUtf8JsonModuleDescription.Resolvers { get { return Resolvers; } }
    }
}
