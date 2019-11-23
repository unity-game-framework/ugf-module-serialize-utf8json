using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Serialize.Utf8Json.Editor.Tests
{
    public class Utf8JsonMathTypeGenerator : ScriptableObject
    {
        [SerializeField] private Object m_folder;
        [SerializeField] private TextAsset m_resolver;

        public Object Folder { get { return m_folder; } set { m_folder = value; } }
        public TextAsset Resolver { get { return m_resolver; } set { m_resolver = value; } }

        [ContextMenu("Generate", false, 1000)]
        public void Generate()
        {
            Generate(m_folder, m_resolver);
        }

        public void Generate(Object folder, TextAsset resolver = null)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));

            string path = AssetDatabase.GetAssetPath(folder);

            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            List<Utf8JsonMathTypeGenerateInfo> infos = Utf8JsonMathTypeGenerateUtility.GetDefaultInfos();

            Utf8JsonMathTypeGenerateUtility.GenerateFiles(infos, path);

            if (resolver != null)
            {
                string resolverPath = AssetDatabase.GetAssetPath(m_resolver);

                Utf8JsonMathTypeGenerateUtility.CollectFiles(resolverPath, path);
            }

            AssetDatabase.Refresh();
        }
    }
}
