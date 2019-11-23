// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

namespace Utf8Json.Runtime.Resolvers.Asset
{
    [global::UnityEngine.CreateAssetMenuAttribute(menuName = ("UGF/Utf8Json/Generated/Utf8Json.Runtime.Resolvers.UtfJsonMathResolver"), order = (2000))]
    public class UtfJsonMathResolverAsset : global::UGF.Utf8Json.Runtime.Resolver.Utf8JsonResolverAsset
    {
        public override global::Utf8Json.IJsonFormatterResolver GetResolver()
        {
            return Utf8Json.Runtime.Resolvers.Resolvers.UtfJsonMathResolver.Instance;
        }
    }
}

namespace Utf8Json.Runtime.Resolvers.Resolvers
{
    using System;
    using Utf8Json;

    public class UtfJsonMathResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new UtfJsonMathResolver();

        UtfJsonMathResolver()
        {

        }

        public global::Utf8Json.IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::Utf8Json.IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var f = UtfJsonMathResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class UtfJsonMathResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static UtfJsonMathResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(0)
            {
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                default: return null;
            }
        }
    }
}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612


