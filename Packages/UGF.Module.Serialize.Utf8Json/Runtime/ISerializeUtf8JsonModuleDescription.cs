using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.Utf8Json.Runtime.Resolver;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public interface ISerializeUtf8JsonModuleDescription : IDescription
    {
        string BytesSerializerName { get; }
        string TextCompactSerializerName { get; }
        string TextReadableSerializerName { get; }
        IReadOnlyList<Utf8JsonResolverAsset> Resolvers { get; }
    }
}
