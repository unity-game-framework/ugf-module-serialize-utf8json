using UGF.Module.Serialize.Runtime;
using UGF.Module.Serialize.Utf8Json.Runtime;
using UnityEngine;

namespace UGF.Module.Serialize.Utf8Json.Editor.Settings
{
    internal class SerializeUtf8JsonEditorSettingsData : ScriptableObject
    {
        [SerializeField] private SerializeModuleInfoAsset m_serializeModuleInfo;
        [SerializeField] private SerializeUtf8JsonModuleInfoAsset m_serializeUtf8JsonModuleInfo;

        public SerializeModuleInfoAsset SerializeModuleInfo { get { return m_serializeModuleInfo; } set { m_serializeModuleInfo = value; } }
        public SerializeUtf8JsonModuleInfoAsset SerializeUtf8JsonModuleInfo { get { return m_serializeUtf8JsonModuleInfo; } set { m_serializeUtf8JsonModuleInfo = value; } }
    }
}
