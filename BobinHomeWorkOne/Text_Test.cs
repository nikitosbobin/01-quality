using System;
using System.Collections.Generic;
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

        [TestCase("__ghbhg", 0, Result = TagType.Bold)]
        [TestCase("_ghbhg", 0, Result = TagType.Italic)]
        [TestCase("`    ghbhg", 0, Result = TagType.Code)]
        [TestCase("\\ ghbhg", 0, Result = TagType.Null)]
        [TestCase("\\ghbhg", 0, Result = TagType.Null)]
        [TestCase("\\___ghbhg", 0, Result = TagType.IgnoreNext)]
        [TestCase("ghbhg __", 6, Result = TagType.Bold)]
        public TagType TestGetTagType(string word, int index)
        {
            return Line.GetTagType(word, ref index).Key;
        }

        //[TestCase("hhh", Result = null)]
        //[TestCase("", Result = null)]
        //[TestCase("___``_", Result = null)]
        //[TestCase("_hhh", Result = new TagType[] { TagType.ItalicOpen })]
        //[TestCase("__hhh", Result = new TagType[] { TagType.BoldOpen })]
        //[TestCase("`hhh", Result = new TagType[] { TagType.CodeOpen })]
        //[TestCase("`_hhh", Result = new TagType[] { TagType.CodeOpen, TagType.ItalicOpen })]
        //[TestCase("`___hhh", Result = new TagType[] { TagType.CodeOpen, TagType.Collision })]
        //[TestCase("\\`___hhh", Result = new TagType[] { TagType.IgnoreOpen, TagType.CodeOpen, TagType.Collision })]
        //public TagType[] TestGetOpeners(string input)
        //{
        //    return Line.GetOpeners(input);
        //}

        //[TestCase("hhh", Result = null)]
        //[TestCase("", Result = null)]
        //[TestCase("hhh`", Result = new TagType[] { TagType.CodeClose })]
        //[TestCase("__`hhh`__", Result = new TagType[] { TagType.CodeClose, TagType.BoldClose })]
        //[TestCase("__`hhh`_\\___", Result = new TagType[] { TagType.CodeClose, TagType.ItalicClose, 
        //    TagType.IgnoreClose, TagType.Collision })]
        //[TestCase("___```", Result = null)]
        //public TagType[] TestGetClosers(string input)
        //{
        //    return Line.GetClosers(input);
        //}
    }
}
