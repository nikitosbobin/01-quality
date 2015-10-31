using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BobinHomeWorkOne
{
    [TestFixture]
    class Text_Test
    {
        [TestCase("hdhk", Result = "hdhk")]
        [TestCase("", Result = "")]
        [TestCase("`hdhk", Result = "`hdhk")]
        [TestCase("`hdhk`", Result = "yyyyyy")]
        [TestCase("hh `hdhk` uuiui`hdhk`utri`ff", Result = "hh yyyyyy uuiuiyyyyyyutri`ff")]
        [TestCase("hhh``hh", Result = "hhhyyhh")]
        public String ShouldDeleteIgnoredLayouts(String input)
        {
            return Layout.ExcludeCode(input);
        }

        [TestCase("hhh", true,  Result = 0)]
        [TestCase("__hhh", true, Result = 2)]
        [TestCase("_`__hh__h", true, Result = 1)]
        [TestCase("hhh", false, Result = 0)]
        [TestCase("hhh__", false, Result = 2)]
        [TestCase("hh__h__`_", false, Result = 1)]
        public int ShouldReturnCountOfDownMarks(String input, bool right)
        {
            return Layout.GetDownCount(input, right);
        }


        [TestCase("_hhh_", Result = "_hhh_")]
        [TestCase("_hh_ _h_", Result = "_hh_")]
        [TestCase("_hh__h_", Result = "_hh__h_")]
        [TestCase("___hgjkfdh_", Result = "")]
        [TestCase("___hgjk`_ghf_`fdh__", Result = "___hgjk`_ghf_`fdh__")]
        [TestCase("hgjk`_ghf_`fdh", Result = "")]
        [TestCase("_hgj\\_kfdh_", Result = "_hgj\\_kfdh_")]
        [TestCase("_hgj\\_ kfdh_", Result = "_hgj_ kfdh_")]
        public String ShouldSplitStringByDownMarks(String input)
        {
            return Layout.SplitOptions(input);
        }
        
    }
}
