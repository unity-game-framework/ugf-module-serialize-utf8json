using System;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public interface ISerializeUtf8JsonUnionProvider
    {
        void Add<TUnionType, TTargetType>(string typeIdentifier) where TTargetType : TUnionType;
        void Add(Type unionType, Type targetType, string typeIdentifier, IJsonFormatter formatter);
        void Add(Type unionType, Type targetType);
        void Remove(Type unionType, Type targetType);
        void Remove(Type unionType);
        void Clear();
        Type GetUnionType(Type targetType);
        bool TryGetUnionType(Type targetType, out Type unionType);
    }
}
