using System.Collections.Generic;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public class SerializeUtf8JsonModuleDescription : ISerializeUtf8JsonModuleDescription
    {
        public string BytesSerializerName { get; set; } = SerializerUtf8JsonUtility.SerializerBytesName;
        public string TextCompactSerializerName { get; set; } = SerializerUtf8JsonUtility.SerializerTextCompactName;
        public string TextReadableSerializerName { get; set; } = SerializerUtf8JsonUtility.SerializerTextReadableName;
        public List<IJsonFormatterResolver> Resolvers { get; set; } = new List<IJsonFormatterResolver>();

        IReadOnlyList<IJsonFormatterResolver> ISerializeUtf8JsonModuleDescription.Resolvers { get { return Resolvers; } }
    }
}
