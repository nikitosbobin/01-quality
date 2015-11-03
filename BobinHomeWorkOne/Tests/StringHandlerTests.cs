using System;
using NUnit.Framework;

namespace BobinHomeWorkOne.Classes
{
    partial class StringHandler
    {
        [TestFixture]
        private class StringHandlerTests
        {
            [TestCase("__abc__", Result = new[] { 2, 6 })]
            [TestCase("_abc_", Result = new[] { 1, 4 })]
            [TestCase("_very interesting information_.", Result = new[] { 1, 29 })]
            [TestCase("abc", Result = new[] { 0, -1 })]
            [TestCase("", Result = new[] { 0, -1 })]
            [TestCase("_d_", Result = new[] { 1, 2 })]
            [TestCase("______abc__", Result = new[] { 0, -1 })]
            [TestCase("___abc_____", Result = new[] { 5, 10 })]
            public static int[] ShouldSplitAnyCombinations(String tested)
            {
                var temp = new StringHandler(String.Empty);
                var result = temp.FindLastMark(tested);
                return new[] { result.Item1, result.Item2 };
            }

            [TestCase("hdhk", Result = "hdhk")]
            [TestCase("", Result = "")]
            [TestCase("`hdhk", Result = "`hdhk")]
            [TestCase("`hdhk`", Result = "yyyyyy")]
            [TestCase("hh `hdhk` uuiui`hdhk`utri`ff", Result = "hh yyyyyy uuiuiyyyyyyutri`ff")]
            [TestCase("hhh``hh", Result = "hhhyyhh")]
            public static String ShouldDeleteIgnoredLayouts(String input)
            {
                var temp = new StringHandler(String.Empty);
                return temp.SimplifyCodeLayout(input);
            }

            [TestCase("hhh", true, Result = 0)]
            [TestCase("__hhh", true, Result = 2)]
            [TestCase("_`__hh__h", true, Result = 1)]
            [TestCase("hhh", false, Result = 0)]
            [TestCase("hhh__", false, Result = 2)]
            [TestCase("hh__h__`_", false, Result = 1)]
            public static int ShouldReturnCountOfDownMarks(String input, bool right)
            {
                return GetDownCount(input, right);
            }

            [TestCase("_hhh_", Result = "_hhh_")]
            [TestCase("_hhhh_ _h_", Result = "_hhhh_")]
            [TestCase("_hh__h_", Result = "_hh__h_")]
            [TestCase("___hgjkfdh_", Result = "")]
            [TestCase("___hgjk`_ghf_`fdh__", Result = "___hgjk`_ghf_`fdh__")]
            [TestCase("hgjk`_ghf_`fdh", Result = "")]
            [TestCase("_hgj\\_kfdh_", Result = "_hgj\\_kfdh_")]
            [TestCase("_hgj\\_ kfdh_", Result = "_hgj\\_ kfdh_")]
            public static String ShouldSplitStringByDownMarks(String input)
            {
                return SplitOptions(input);
            }

            [TestCase("_hgj\\_ kfdh_", Result = "_hgj\\_ kfdh_")]
            [TestCase("_hgj_ kfdh_", Result = "_hgj_ ")]
            [TestCase("_hgj_ kfdh__", Result = "_hgj_ ")]
            [TestCase("_hgj_ kfdh\\_", Result = "_hgj_ ")]
            [TestCase("_hgj _kfdh_", Result = "")]
            [TestCase("_hgj_kfdh_", Result = "_hgj_kfdh_")]
            [TestCase("_hgj_kfdh", Result = "")]
            [TestCase("_abc __dfg__ nhy __jki__ fgtrf_.", Result = "_abc __dfg__ nhy __jki__ fgtrf_.")]
            [TestCase("_abc __dfg__ nhy __jki_ fgtrf_.", Result = "_abc __dfg__ nhy __jki_ ")]
            public static String ShouldHandleOneMarkVeryAccurate(String input)
            {
                return SplitWithMoreAccuracy(input);
            }

            [TestCase("hhh``hh", 0, LayoutType.Null, "", 0 )]
            [TestCase("_word_", 0, LayoutType.Italic, "word", 6 )]
            [TestCase("__word__", 0, LayoutType.Bold, "word", 8 )]
            [TestCase("___word\\_ next___", 0, LayoutType.Italic, "__word\\_ next__", 17 )]
            [TestCase("_hgj_ kfdh_", 0, LayoutType.Italic, "hgj", 5 )]
            [TestCase("_hgj_kfdh", 0, LayoutType.Null, "", 0 )]
            [TestCase("_hgj`hghgh_ fhgjkfd`ghfdj_", 0, LayoutType.Italic, "hgj`hghgh_ fhgjkfd`ghfdj", 26 )]
            public static void ShouldRightDetermineTypeOfWord(String testedWord, int index, 
                LayoutType expectedType, String expectedWord, int expectedLastIndex)
            {
                var temp = new StringHandler(testedWord);
                var result = temp.GetNextUnderbar(index);
                Assert.AreEqual(expectedType ,result.Type);
                Assert.AreEqual(expectedWord ,result.CleanedWord);
                Assert.AreEqual(expectedLastIndex, result.LastIndex);
            }

            [TestCase("_hgj_ kfdh_", 1, LayoutType.Italic ,"hgj", 5 )]
            [TestCase("__hghhhj__", 1, LayoutType.Bold, "hghhhj", 10 )]
            [TestCase("___hhj__", 1, LayoutType.Italic, "__hhj_", 8 )]
            [TestCase("_hhj__", 1, LayoutType.Simple, "_hhj__", 6 )]
            [TestCase("__hhj___", 1,LayoutType.Bold, "hhj_", 8 )]
            public static void ShouldMoveIteratorInWord(String testedWord, int movesCount,
                LayoutType expectedType, String expectedWord, int expectedLastIndex)
            {
                var temp = new StringHandler(testedWord);
                for (int i = 0; i < movesCount; ++i)
                    temp.MoveIterator();
                var result = temp.oneLevel.GetLastPair();
                Assert.AreEqual(expectedType, result.Item1);
                Assert.AreEqual(expectedWord, result.Item2);
                Assert.AreEqual(expectedLastIndex, temp._iterator);
            }
        }
    }
}
