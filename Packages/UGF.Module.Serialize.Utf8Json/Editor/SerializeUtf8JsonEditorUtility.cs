using System;
using UGF.Logs.Runtime;
using UGF.Module.Serialize.Runtime;
using UGF.Module.Serialize.Utf8Json.Editor.Settings;
using UGF.Module.Serialize.Utf8Json.Runtime;
using UGF.Serialize.Runtime;

namespace UGF.Module.Serialize.Utf8Json.Editor
{
    public static class SerializeUtf8JsonEditorUtility
    {
        public static ISerializeUtf8JsonModule SerializeUtf8JsonModule { get { return m_serializeUtf8JsonModule ?? (m_serializeUtf8JsonModule = CreateModule()); } set { m_serializeUtf8JsonModule = value; } }

        private static ISerializeUtf8JsonModule m_serializeUtf8JsonModule;

        public static string ToJson<T>(T target, bool readable = false)
        {
            ISerializeUtf8JsonModule module = SerializeUtf8JsonModule;
            ISerializer<string> serializer = GetSerializer(module, readable);

            return serializer.Serialize(target);
        }

        public static string ToJson(object target, bool readable = false)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            ISerializeUtf8JsonModule module = SerializeUtf8JsonModule;
            ISerializer<string> serializer = GetSerializer(module, readable);

            return serializer.Serialize(target);
        }

        public static T FromJson<T>(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            ISerializeUtf8JsonModule module = SerializeUtf8JsonModule;
            ISerializer<string> serializer = GetSerializer(module, false);

            return serializer.Deserialize<T>(text);
        }

        public static object FromJson(string text, Type targetType)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("Value cannot be null or empty.", nameof(text));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            ISerializeUtf8JsonModule module = SerializeUtf8JsonModule;
            ISerializer<string> serializer = GetSerializer(module, false);

            return serializer.Deserialize(targetType, text);
        }

        private static ISerializeUtf8JsonModule CreateModule()
        {
            SerializeModuleInfoAsset serializeModuleInfo = SerializeUtf8JsonEditorSettings.SerializeModuleInfo;
            SerializeUtf8JsonModuleInfoAsset serializeUtf8JsonModuleInfo = SerializeUtf8JsonEditorSettings.SerializeUtf8JsonModuleInfo;

            if (serializeModuleInfo == null) throw new ArgumentException("A serialize module not specified in editor settings.");
            if (serializeUtf8JsonModuleInfo == null) throw new ArgumentException("A Utf8Json module not specified in editor settings.");

            ISerializeModuleDescription serializeModuleDescription = serializeModuleInfo.GetDescription();
            ISerializeUtf8JsonModuleDescription serializeUtf8JsonModuleDescription = serializeUtf8JsonModuleInfo.GetDescription();

            return CreateModule(serializeModuleDescription, serializeUtf8JsonModuleDescription);
        }

        private static ISerializeUtf8JsonModule CreateModule(ISerializeModuleDescription serializeModuleDescription, ISerializeUtf8JsonModuleDescription utf8JsonModuleDescription)
        {
            if (serializeModuleDescription == null) throw new ArgumentNullException(nameof(serializeModuleDescription));
            if (utf8JsonModuleDescription == null) throw new ArgumentNullException(nameof(utf8JsonModuleDescription));

            var serializeModule = new SerializeModule(serializeModuleDescription);
            var serializeUtf8JsonModule = new SerializeUtf8JsonModule(serializeModule, utf8JsonModuleDescription);

            using (new LogEnableScope(false))
            {
                serializeModule.Initialize();
                serializeUtf8JsonModule.Initialize();
            }

            return serializeUtf8JsonModule;
        }

        private static ISerializer<string> GetSerializer(ISerializeUtf8JsonModule module, bool readable)
        {
            string name = readable
                ? module.SerializeModule.Description.DefaultTextReadableSerializerName
                : module.SerializeModule.Description.DefaultTextCompactSerializerName;

            return module.SerializeModule.Provider.Get<string>(name);
        }
    }
}
