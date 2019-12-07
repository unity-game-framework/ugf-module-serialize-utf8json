using UnityEngine;

namespace UGF.Module.Serialize.Utf8Json.Runtime.TypeRegisters
{
    public abstract class SerializeUtf8JsonTypeRegisterAsset : ScriptableObject
    {
        public abstract ISerializeUtf8JsonTypeRegister GetTypeRegister();
    }
}
