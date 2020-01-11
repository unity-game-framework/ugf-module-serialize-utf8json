using System;
using System.Collections.Generic;
using System.IO;
using UGF.AssetPipeline.Editor.Asset.Info;
using UGF.Code.Generate.Editor.Container.Info;
using UGF.Utf8Json.Editor.ExternalType;
using UGF.Utf8Json.Editor.Resolver;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Serialize.Utf8Json.Editor.Tests
{
    public static class Utf8JsonMathTypeGenerateUtility
    {
        public static void CollectFiles(string resolverPath, string folderPath)
        {
            if (string.IsNullOrEmpty(resolverPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(resolverPath));
            if (string.IsNullOrEmpty(folderPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(folderPath));

            var resolver = AssetInfoEditorUtility.LoadInfo<Utf8JsonResolverAssetInfo>(resolverPath);
            string[] paths = Directory.GetFiles(folderPath, $"*.{Utf8JsonExternalTypeEditorUtility.EXTERNAL_TYPE_ASSET_EXTENSION_NAME}", SearchOption.AllDirectories);

            resolver.Sources.Clear();

            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);

                if (asset != null)
                {
                    resolver.Sources.Add(asset);
                }
            }

            AssetInfoEditorUtility.SaveInfo(resolverPath, resolver, false);
        }

        public static void GenerateFiles(List<Utf8JsonMathTypeGenerateInfo> infos, string folderPath)
        {
            if (infos == null) throw new ArgumentNullException(nameof(infos));
            if (string.IsNullOrEmpty(folderPath)) throw new ArgumentNullException(nameof(folderPath));

            Directory.CreateDirectory(folderPath);

            for (int i = 0; i < infos.Count; i++)
            {
                Utf8JsonMathTypeGenerateInfo info = infos[i];
                CodeGenerateContainerInfo assetInfo = CreateAssetInfo(info);
                string path = $"{folderPath}/{info.Type.Name}.{Utf8JsonExternalTypeEditorUtility.EXTERNAL_TYPE_ASSET_EXTENSION_NAME}";
                string content = EditorJsonUtility.ToJson(assetInfo, true);

                File.WriteAllText(path, content);
            }
        }

        public static List<Utf8JsonMathTypeGenerateInfo> GetDefaultInfos()
        {
            return new List<Utf8JsonMathTypeGenerateInfo>
            {
                new Utf8JsonMathTypeGenerateInfo(typeof(bool2), 2),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool2x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool2x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool2x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool3), 3),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool3x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool3x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool3x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool4), 4),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool4x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool4x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(bool4x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double2), 2),
                new Utf8JsonMathTypeGenerateInfo(typeof(double2x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double2x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double2x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double3), 3),
                new Utf8JsonMathTypeGenerateInfo(typeof(double3x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double3x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double3x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double4), 4),
                new Utf8JsonMathTypeGenerateInfo(typeof(double4x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double4x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(double4x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float2), 2),
                new Utf8JsonMathTypeGenerateInfo(typeof(float2x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float2x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float2x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float3), 3),
                new Utf8JsonMathTypeGenerateInfo(typeof(float3x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float3x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float3x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float4), 4),
                new Utf8JsonMathTypeGenerateInfo(typeof(float4x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float4x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(float4x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(half2), 2),
                new Utf8JsonMathTypeGenerateInfo(typeof(half3), 3),
                new Utf8JsonMathTypeGenerateInfo(typeof(half4), 4),
                new Utf8JsonMathTypeGenerateInfo(typeof(int2), 2),
                new Utf8JsonMathTypeGenerateInfo(typeof(int2x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int2x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int2x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int3), 3),
                new Utf8JsonMathTypeGenerateInfo(typeof(int3x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int3x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int3x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int4), 4),
                new Utf8JsonMathTypeGenerateInfo(typeof(int4x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int4x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(int4x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint2), 2),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint2x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint2x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint2x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint3), 3),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint3x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint3x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint3x4), 4, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint4), 4),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint4x2), 2, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint4x3), 3, false),
                new Utf8JsonMathTypeGenerateInfo(typeof(uint4x4), 4, false),
            };
        }

        private static CodeGenerateContainerInfo CreateAssetInfo(Utf8JsonMathTypeGenerateInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            var assetInfo = new CodeGenerateContainerInfo { TypeName = info.Type.AssemblyQualifiedName };

            for (int i = 0; i < info.Count; i++)
            {
                CodeGenerateContainerInfo.MemberInfo member = CreateMember(i, info.IsVector);

                assetInfo.Members.Add(member);
            }

            return assetInfo;
        }

        private static CodeGenerateContainerInfo.MemberInfo CreateMember(int index, bool isVector)
        {
            return new CodeGenerateContainerInfo.MemberInfo()
            {
                Name = GetName(index, isVector)
            };
        }

        private static string GetName(int index, bool isVector)
        {
            switch (index)
            {
                case 0: return isVector ? "x" : "c0";
                case 1: return isVector ? "y" : "c1";
                case 2: return isVector ? "z" : "c2";
                case 3: return isVector ? "w" : "c3";
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
