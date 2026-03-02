using NUnit.Framework;
using UnityEngine;

namespace UnityCalculator.Persistence
{
    [TestFixture]
    public class PlayerPrefsPersistenceProviderTest
    {
        [SetUp]
        public void SetUp()
        {
            PlayerPrefs.DeleteAll();
        }

        [Test]
        public void Load_WhenKeyNotExists_ReturnsDefault()
        {
            var sut = CreateProvider();

            var actual = sut.Load("missing_key", "default");

            Assert.That(actual, Is.EqualTo("default"));
        }

        [Test]
        public void Save_ThenLoad_ReturnsValue()
        {
            var sut = CreateProvider();

            sut.Save("key", "value");
            var actual = sut.Load("key", string.Empty);

            Assert.That(actual, Is.EqualTo("value"));
        }

        [Test]
        public void HasKey_WhenKeyExists_ReturnsTrue()
        {
            var sut = CreateProvider();
            sut.Save("key", "value");

            var actual = sut.HasKey("key");

            Assert.That(actual, Is.True);
        }

        [Test]
        public void HasKey_WhenKeyNotExists_ReturnsFalse()
        {
            var sut = CreateProvider();

            var actual = sut.HasKey("missing_key");

            Assert.That(actual, Is.False);
        }

        private PlayerPrefsPersistenceProvider CreateProvider()
        {
            return new PlayerPrefsPersistenceProvider();
        }
    }
}
