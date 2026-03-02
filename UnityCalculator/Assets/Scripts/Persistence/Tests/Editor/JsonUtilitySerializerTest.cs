using System;
using NUnit.Framework;

namespace UnityCalculator.Persistence
{
    [TestFixture]
    public class JsonUtilitySerializerTest
    {
        [Test]
        public void Serialize_ThenDeserialize_RestoresData()
        {
            var sut = CreateSerializer();
            var data = new TestData { Name = "calc", Value = 42 };

            var json = sut.Serialize(data);
            var actual = sut.Deserialize<TestData>(json);

            Assert.That(actual.Name, Is.EqualTo("calc"));
        }

        [Test]
        public void Serialize_ThenDeserialize_RestoresNumericField()
        {
            var sut = CreateSerializer();
            var data = new TestData { Name = string.Empty, Value = 99 };

            var json = sut.Serialize(data);
            var actual = sut.Deserialize<TestData>(json);

            Assert.That(actual.Value, Is.EqualTo(99));
        }

        private JsonUtilitySerializer CreateSerializer()
        {
            return new JsonUtilitySerializer();
        }

        [Serializable]
        private class TestData
        {
            public string Name;
            public int Value;
        }
    }
}
