using System.Collections.Generic;
using UGF.Utf8Json.Runtime;

namespace UGF.Module.Serialize.Utf8Json.Runtime.TypeRegisters
{
    public interface ISerializeUtf8JsonTypeRegister
    {
        void Register(IUtf8JsonFormatterResolver resolver, ISerializeUtf8JsonUnionProvider unionProvider);
        void UnRegister(IUtf8JsonFormatterResolver resolver, ISerializeUtf8JsonUnionProvider unionProvider);
    }
}
