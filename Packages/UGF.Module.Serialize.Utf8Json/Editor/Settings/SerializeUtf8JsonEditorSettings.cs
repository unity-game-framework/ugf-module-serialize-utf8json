using UGF.CustomSettings.Editor;
using UGF.CustomSettings.Runtime;
using UGF.Module.Serialize.Runtime;
using UGF.Module.Serialize.Utf8Json.Runtime;
using UnityEditor;

namespace UGF.Module.Serialize.Utf8Json.Editor.Settings
{
    public static class SerializeUtf8JsonEditorSettings
    {
        public static SerializeModuleInfoAsset SerializeModuleInfo
        {
            get { return m_settings.Data.SerializeModuleInfo; }
            set
            {
                m_settings.Data.SerializeModuleInfo = value;
                m_settings.SaveSettings();
            }
        }

        public static SerializeUtf8JsonModuleInfoAsset SerializeUtf8JsonModuleInfo
        {
            get { return m_settings.Data.SerializeUtf8JsonModuleInfo; }
            set
            {
                m_settings.Data.SerializeUtf8JsonModuleInfo = value;
                m_settings.SaveSettings();
            }
        }

        private static readonly CustomSettings<SerializeUtf8JsonEditorSettingsData> m_settings = new CustomSettingsEditorPackage<SerializeUtf8JsonEditorSettingsData>
        (
            "UGF.Module.Serialize.Utf8Json",
            "SerializeUtf8JsonEditorSettings"
        );

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<SerializeUtf8JsonEditorSettingsData>("Project/UGF/Serialize Utf8Json", m_settings, SettingsScope.Project);
        }
    }
}
