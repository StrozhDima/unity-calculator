using System.Collections.Generic;
using NUnit.Framework;

namespace UnityCalculator.Calculator
{
    [TestFixture]
    public class HistoryModelTest
    {
        [Test]
        public void Add_WhenCalledMultipleTimes_NewestEntryIsFirst()
        {
            IHistoryModel sut = new HistoryModel();

            sut.Add(new HistoryEntry("1+1", "2", CalculationResultType.Success));
            sut.Add(new HistoryEntry("2+2", "4", CalculationResultType.Success));

            Assert.That(sut.Entries.Value[0].Expression, Is.EqualTo("2+2"));
        }

        [Test]
        public void SetAll_WithNewEntries_ReplacesAllExistingEntries()
        {
            IHistoryModel sut = new HistoryModel();
            sut.Add(new HistoryEntry("1+1", "2", CalculationResultType.Success));
            var newEntries = new List<HistoryEntry> { new("5+5", "10", CalculationResultType.Success) };
            sut.SetAll(newEntries);

            Assert.That(sut.Entries.Value, Has.Count.EqualTo(1));
        }
    }
}