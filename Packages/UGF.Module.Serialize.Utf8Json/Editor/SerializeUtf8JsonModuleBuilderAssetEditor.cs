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
        private ReorderableList m_list;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_propertyBytesSerializerName = serializedObject.FindProperty("m_bytesSerializerName");
            m_propertyTextCompactSerializerName = serializedObject.FindProperty("m_textCompactSerializerName");
            m_propertyTextReadableSerializerName = serializedObject.FindProperty("m_textReadableSerializerName");

            SerializedProperty propertyResolvers = serializedObject.FindProperty("m_resolverAssets");

            m_list = new ReorderableList(serializedObject, propertyResolvers);
            m_list.drawHeaderCallback = OnDrawHeader;
            m_list.drawElementCallback = OnDrawElement;
            m_list.elementHeightCallback = OnElementHeight;
            m_list.onAddCallback = OnAdd;
        }

        protected override void DrawDescription()
        {
            EditorGUILayout.PropertyField(m_propertyBytesSerializerName);
            EditorGUILayout.PropertyField(m_propertyTextCompactSerializerName);
            EditorGUILayout.PropertyField(m_propertyTextReadableSerializerName);

            EditorGUILayout.Space();

            m_list.DoLayoutList();
        }

        protected override void DrawInspector()
        {
        }

        private void OnDrawHeader(Rect rect)
        {
            GUI.Label(rect, $"Resolvers (Size: {m_list.count})", EditorStyles.boldLabel);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyElement = m_list.serializedProperty.GetArrayElementAtIndex(index);

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
