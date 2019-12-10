using System;
using UGF.Serialize.Runtime;
using UGF.Utf8Json.Runtime;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public class SerializerUtf8JsonBytes : SerializerBase<byte[]>
    {
        public IUtf8JsonFormatterResolver Resolver { get; }

        public SerializerUtf8JsonBytes(IUtf8JsonFormatterResolver resolver)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public override byte[] Serialize<T>(T target)
        {
            var writer = new JsonWriter();
            IJsonFormatter<T> formatter = Resolver.GetFormatter<T>();

            formatter.Serialize(ref writer, target, Resolver);

            return writer.ToUtf8ByteArray();
        }

        public override T Deserialize<T>(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var reader = new JsonReader(data);
            IJsonFormatter<T> formatter = Resolver.GetFormatter<T>();

            return formatter.Deserialize(ref reader, Resolver);
        }

        public override byte[] Serialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            Type targetType = target.GetType();
            var writer = new JsonWriter();
            var formatter = (IJsonFormatter<object>)Resolver.GetFormatter(targetType);

            formatter.Serialize(ref writer, target, Resolver);

            return writer.ToUtf8ByteArray();
        }

        public override object Deserialize(Type targetType, byte[] data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            var reader = new JsonReader(data);
            var formatter = (IJsonFormatter<object>)Resolver.GetFormatter(targetType);

            return formatter.Deserialize(ref reader, Resolver);
        }
    }
}
