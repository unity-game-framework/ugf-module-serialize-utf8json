using System.Collections.Generic;
using UGF.Module.Serialize.Utf8Json.Runtime.TypeRegisters;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public interface ISerializeUtf8JsonModuleDescription
    {
        string BytesSerializerName { get; }
        string TextCompactSerializerName { get; }
        string TextReadableSerializerName { get; }
        IReadOnlyList<IJsonFormatterResolver> Resolvers { get; }
        IReadOnlyList<ISerializeUtf8JsonTypeRegister> TypeRegisters { get; }
    }
}
