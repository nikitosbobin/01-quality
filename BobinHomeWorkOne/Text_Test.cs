using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BobinHomeWorkOne
{
    [TestFixture]
    class Text_Test
    {
        [TestCase("__jhd__", 0, Result = new int[]{ 2, 2, 5 })]
        [TestCase("__jhd __", 0, Result = new int[] { 2, 0, -1 })]
        [TestCase("__jhd __ gfdg__", 0, Result = new int[] { 2, 2, 13 })]
        [TestCase("jhd__", 0, Result = new int[] { 0, 2, 3 })]
        [TestCase("jhd __", 0, Result = new int[] { 0, 0, -1 })]
        public int[] TestGetEndOfUnderscore(string word, int start)
        {
            var result = new int[3];
            var tmp = Layout.GetEndOfUnderscore(word, start);
            result[0] = tmp.Item1;
            result[1] = tmp.Item2;
            result[2] = tmp.Item3;
            return result;
        }
    }
}
