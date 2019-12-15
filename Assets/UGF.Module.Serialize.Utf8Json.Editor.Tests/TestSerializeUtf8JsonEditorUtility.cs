using NUnit.Framework;
using UnityEngine;

namespace UGF.Module.Serialize.Utf8Json.Editor.Tests
{
    public class TestSerializeUtf8JsonEditorUtility
    {
        [Test]
        public void ToJson()
        {
            var target = new Vector2(10F, 10F);
            string text = SerializeUtf8JsonEditorUtility.ToJson(target);

            Assert.NotNull(text);
            Assert.AreEqual("{\"x\":10.0,\"y\":10.0}", text);
        }

        [Test]
        public void FromJson()
        {
            string text = "{\"x\":10.0,\"y\":10.0}";
            var target = SerializeUtf8JsonEditorUtility.FromJson<Vector2>(text);

            Assert.AreEqual(10, target.x);
            Assert.AreEqual(10, target.y);
        }
    }
}
