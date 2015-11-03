using System;
using NUnit.Framework;

namespace BobinHomeWorkOne.Classes
{
    [TestFixture]
    class TextTests
    {
        [Test]
        public static void ShouldDivideOnUnits()
        {
            var exampleText = new[]
            {
                "aaaaaaaaaaa ",
                "bbbbbbbbbbbbbb",
                "   ",
                "cccccccccc ",
                "ddddddddddddd",
                "",
                "",
                "",
                "eeeeeeee"
            };
            var expected = new[]
            {
                "<html xml:lang=\"ru\" lang=\"ru\">",
                "<meta content=\"text/html; charset=UTF-8\">",
                "<body>",
                "<p>\r\naaaaaaaaaaa bbbbbbbbbbbbbb\r\n</p>",
                "<p>\r\ncccccccccc ddddddddddddd\r\n</p>",
                "<p>\r\neeeeeeee\r\n</p>",
                "</body>",
                "</html>"
            };
            var actual = new Text(exampleText);
            CollectionAssert.AreEqual(expected, actual.GetResultText());
        }

        [Test]
        public static void ShouldCreateEmptyText()
        {
            var exampleText = new[]{""};
            var expected = new[]
            {
                "<html xml:lang=\"ru\" lang=\"ru\">",
                "<meta content=\"text/html; charset=UTF-8\">",
                "<body>",
                "<p>\r\n\r\n</p>",
                "</body>",
                "</html>"
            };
            var actual = new Text(exampleText);
            CollectionAssert.AreEqual(expected, actual.GetResultText());
        }
    }
}
