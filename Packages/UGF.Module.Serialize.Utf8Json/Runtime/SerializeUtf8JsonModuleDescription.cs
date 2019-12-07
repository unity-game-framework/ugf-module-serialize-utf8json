using System.Collections.Generic;
using UGF.Module.Serialize.Utf8Json.Runtime.TypeRegisters;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public class SerializeUtf8JsonModuleDescription : ISerializeUtf8JsonModuleDescription
    {
        public string BytesSerializerName { get; set; } = SerializerUtf8JsonUtility.SerializerBytesName;
        public string TextCompactSerializerName { get; set; } = SerializerUtf8JsonUtility.SerializerTextCompactName;
        public string TextReadableSerializerName { get; set; } = SerializerUtf8JsonUtility.SerializerTextReadableName;
        public List<IJsonFormatterResolver> Resolvers { get; } = new List<IJsonFormatterResolver>();
        public List<ISerializeUtf8JsonTypeRegister> TypeRegisters { get; } = new List<ISerializeUtf8JsonTypeRegister>();

        IReadOnlyList<IJsonFormatterResolver> ISerializeUtf8JsonModuleDescription.Resolvers { get { return Resolvers; } }
        IReadOnlyList<ISerializeUtf8JsonTypeRegister> ISerializeUtf8JsonModuleDescription.TypeRegisters { get { return TypeRegisters; } }
    }
}
