using System;
using System.Text;
using UGF.Serialize.Runtime;
using UGF.Utf8Json.Runtime;
using Utf8Json;

namespace UGF.Module.Serialize.Utf8Json.Runtime
{
    public class SerializerUtf8Json : SerializerBase<string>
    {
        public IUtf8JsonFormatterResolver Resolver { get; }
        public bool Readable { get; }

        public SerializerUtf8Json(IUtf8JsonFormatterResolver resolver, bool readable)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            Readable = readable;
        }

        public override string Serialize<T>(T target)
        {
            var writer = new JsonWriter();
            IJsonFormatter<T> formatter = Resolver.GetFormatter<T>();

            formatter.Serialize(ref writer, target, Resolver);

            string text = Readable
                ? JsonSerializer.PrettyPrint(writer.ToUtf8ByteArray())
                : writer.ToString();

            return text;
        }

        public override T Deserialize<T>(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            var reader = new JsonReader(bytes);
            IJsonFormatter<T> formatter = Resolver.GetFormatter<T>();

            return formatter.Deserialize(ref reader, Resolver);
        }

        public override string Serialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            Type targetType = target.GetType();
            var writer = new JsonWriter();
            var formatter = (IJsonFormatter<object>)Resolver.GetFormatter(targetType);

            formatter.Serialize(ref writer, target, Resolver);

            string text = Readable
                ? JsonSerializer.PrettyPrint(writer.ToUtf8ByteArray())
                : writer.ToString();

            return text;
        }

        public override object Deserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            var reader = new JsonReader(bytes);
            var formatter = (IJsonFormatter<object>)Resolver.GetFormatter(targetType);

            return formatter.Deserialize(ref reader, Resolver);
        }
    }
}
