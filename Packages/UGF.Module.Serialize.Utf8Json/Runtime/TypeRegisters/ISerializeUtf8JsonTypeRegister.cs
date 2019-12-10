using UGF.Utf8Json.Runtime;

namespace UGF.Module.Serialize.Utf8Json.Runtime.TypeRegisters
{
    public interface ISerializeUtf8JsonTypeRegister
    {
        void Register(IUtf8JsonFormatterResolver resolver);
        void UnRegister(IUtf8JsonFormatterResolver resolver);
    }
}
