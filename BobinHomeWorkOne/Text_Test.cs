using System;
using NUnit.Framework;

namespace BobinHomeWorkOne
{
    [TestFixture]
    class Text_Test
    {
        [TestCase("__ghbhg", 0, Result = 2)]
        [TestCase("___`_ghbhg", 0, Result = 3)]
        [TestCase("ghbhg", 0, Result = 0)]
        public int TestGetDownCount(string word, int start)
        {
            return Line.GetDownCount(word, start);
        }

        [TestCase("hhh", Result = null)]
        [TestCase("___``_", Result = null)]
        [TestCase("_hhh", Result = new wordsSetType[] { wordsSetType.ItalicOpen })]
        [TestCase("__hhh", Result = new wordsSetType[] { wordsSetType.BoldOpen })]
        [TestCase("`hhh", Result = new wordsSetType[] { wordsSetType.CodeOpen })]
        [TestCase("`_hhh", Result = new wordsSetType[] { wordsSetType.CodeOpen, wordsSetType.ItalicOpen })]
        [TestCase("`___hhh", Result = new wordsSetType[] { wordsSetType.CodeOpen, wordsSetType.Collision })]
        [TestCase("\\`___hhh", Result = new wordsSetType[] { wordsSetType.IgnoreOpen, wordsSetType.CodeOpen, wordsSetType.Collision })]
        public wordsSetType[] TestGetOpeners(string input)
        {
            return Line.GetOpeners(input);
        }
    }
}
