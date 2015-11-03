using System;
using NUnit.Framework;

namespace BobinHomeWorkOne.Classes
{
    [TestFixture]
    class PairsCollectionTests
    {
        [Test]
        public void GetLastPairInCollection()
        {
            var actually = new PairsCollection<string, int>();
            actually.Add("Item", 55);
            actually.Add("Item2", 56);
            Assert.AreEqual(Tuple.Create("Item2", 56), actually.GetLastPair());
        }

        [Test]
        public void ShouldNotGetLastPairInEmptyCollection()
        {
            var actually = new PairsCollection<string, int>();
            Assert.AreEqual(null, actually.GetLastPair());
        }

        [TestCase("hdhk", 55, Result = true)]
        [TestCase(null, 55, Result = false)]
        [TestCase("hdhk", null, Result = true)]
        public bool ShouldAddDataInCollection(String word, int value)
        {
            var actually = new PairsCollection<string, int>();
            return actually.Add(word, value);
        }

        [TestCase("hdhk", null, Result = "hdhk")]
        [TestCase(null, null, Result = null)]
        public String ShouldWorkingWithIndex(String word, int value)
        {
            var actually = new PairsCollection<string, int>();
            actually.Add(word, value);
            if (actually.Length > 0)
                return actually[0].Item1;
            return null;
        }
    }
}
