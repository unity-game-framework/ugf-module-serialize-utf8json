using UGF.Module.Editor;
using UGF.Module.Serialize.Utf8Json.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.Module.Serialize.Utf8Json.Editor
{
    [CustomEditor(typeof(SerializeUtf8JsonModuleBuilderAsset), true)]
    internal class SerializeUtf8JsonModuleBuilderAssetEditor : ModuleBuilderAssetEditor
    {
        private SerializedProperty m_propertyBytesSerializerName;
        private SerializedProperty m_propertyTextCompactSerializerName;
        private SerializedProperty m_propertyTextReadableSerializerName;
        private ReorderableList m_resolvers;
        private ReorderableList m_registers;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_propertyBytesSerializerName = serializedObject.FindProperty("m_bytesSerializerName");
            m_propertyTextCompactSerializerName = serializedObject.FindProperty("m_textCompactSerializerName");
            m_propertyTextReadableSerializerName = serializedObject.FindProperty("m_textReadableSerializerName");

            SerializedProperty propertyResolvers = serializedObject.FindProperty("m_resolverAssets");
            SerializedProperty propertyRegisters = serializedObject.FindProperty("m_typeRegisterAssets");

            m_resolvers = new ReorderableList(serializedObject, propertyResolvers);
            m_resolvers.drawHeaderCallback = OnDrawHeaderResolvers;
            m_resolvers.drawElementCallback = OnDrawElementResolvers;
            m_resolvers.elementHeightCallback = OnElementHeight;
            m_resolvers.onAddCallback = OnAdd;

            m_registers = new ReorderableList(serializedObject, propertyRegisters);
            m_registers.drawHeaderCallback = OnDrawHeaderRegisters;
            m_registers.drawElementCallback = OnDrawElementRegisters;
            m_registers.elementHeightCallback = OnElementHeight;
            m_registers.onAddCallback = OnAdd;
        }

        protected override void DrawDescription()
        {
            EditorGUILayout.PropertyField(m_propertyBytesSerializerName);
            EditorGUILayout.PropertyField(m_propertyTextCompactSerializerName);
            EditorGUILayout.PropertyField(m_propertyTextReadableSerializerName);

            EditorGUILayout.Space();

            m_resolvers.DoLayoutList();
            m_registers.DoLayoutList();
        }

        protected override void DrawInspector()
        {
        }

        private void OnDrawHeaderResolvers(Rect rect)
        {
            GUI.Label(rect, $"Resolvers (Size: {m_resolvers.count.ToString()})", EditorStyles.boldLabel);
        }

        private void OnDrawHeaderRegisters(Rect rect)
        {
            GUI.Label(rect, $"Type Registers (Size: {m_registers.count.ToString()})", EditorStyles.boldLabel);
        }

        private void OnDrawElementResolvers(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyElement = m_resolvers.serializedProperty.GetArrayElementAtIndex(index);

            DrawElement(rect, propertyElement);
        }

        private void OnDrawElementRegisters(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyElement = m_registers.serializedProperty.GetArrayElementAtIndex(index);

            DrawElement(rect, propertyElement);
        }

        private void DrawElement(Rect rect, SerializedProperty propertyElement)
        {
            rect.y += EditorGUIUtility.standardVerticalSpacing;
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(rect, propertyElement, GUIContent.none);
        }

        private float OnElementHeight(int index)
        {
            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;

            return height + spacing * 2F;
        }

        private void OnAdd(ReorderableList list)
        {
            SerializedProperty propertyModules = list.serializedProperty;

            propertyModules.InsertArrayElementAtIndex(propertyModules.arraySize);

            SerializedProperty propertyElement = propertyModules.GetArrayElementAtIndex(propertyModules.arraySize - 1);

            propertyElement.objectReferenceValue = null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
