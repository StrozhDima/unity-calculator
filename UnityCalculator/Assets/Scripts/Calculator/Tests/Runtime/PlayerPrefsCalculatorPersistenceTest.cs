using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityCalculator.Persistence;

namespace UnityCalculator.Calculator
{
    [TestFixture]
    public class PlayerPrefsCalculatorPersistenceTest
    {
        [SetUp]
        public void SetUp()
        {
            PlayerPrefs.DeleteAll();
        }

        [Test]
        public void Load_WhenNoData_ReturnsEmptyExpression()
        {
            var sut = CreatePersistence();

            var actual = sut.Load();

            Assert.That(actual.CurrentExpression, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Load_WhenNoData_ReturnsEmptyHistory()
        {
            var sut = CreatePersistence();

            var actual = sut.Load();

            Assert.That(actual.History, Has.Count.EqualTo(0));
        }

        [Test]
        public void Save_ThenLoad_RestoresExpression()
        {
            var sut = CreatePersistence();
            var state = new CalculatorState("54+21", new List<HistoryEntry>());

            sut.Save(state);
            var actual = sut.Load();

            Assert.That(actual.CurrentExpression, Is.EqualTo("54+21"));
        }

        [Test]
        public void Save_ThenLoad_RestoresHistory()
        {
            var sut = CreatePersistence();
            var history = new List<HistoryEntry>
            {
                new HistoryEntry("1+1", "2", CalculationResultType.Success)
            };
            var state = new CalculatorState(string.Empty, history);

            sut.Save(state);
            var actual = sut.Load();

            Assert.That(actual.History, Has.Count.EqualTo(1));
        }

        [Test]
        public void Save_ThenLoad_RestoresHistoryEntryData()
        {
            var sut = CreatePersistence();
            var history = new List<HistoryEntry>
            {
                new HistoryEntry("1+1", "2", CalculationResultType.Success)
            };
            var state = new CalculatorState(string.Empty, history);

            sut.Save(state);
            var actual = sut.Load();

            Assert.That(actual.History[0].Expression, Is.EqualTo("1+1"));
        }

        private PlayerPrefsCalculatorPersistence CreatePersistence()
        {
            return new PlayerPrefsCalculatorPersistence(
                new PlayerPrefsPersistenceProvider(),
                new JsonUtilitySerializer()
            );
        }
    }
}
