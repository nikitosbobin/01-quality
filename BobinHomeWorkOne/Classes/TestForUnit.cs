using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace BobinHomeWorkOne.Classes
{
    partial class Unit
    {
        [TestFixture]
        class TestForUnit
        {
            [Test]
            public static void ShouldBuildUnit()
            {
                var oneUnit = new List<String>();
                oneUnit.Add("Текст _окруженный с двух сторон_  одинарными символами подчерка ");
                oneUnit.Add("\\_Вот это\\_, не должно выделиться тегом <em>. ");
                oneUnit.Add("__Двумя символами__ — должен становиться жирным с помощью тега <strong>. ");
                oneUnit.Add("Текст окруженный `одинарными _обратными_ кавычками` должен попадать в тег <code>");
                var result = new Unit(oneUnit);
                var expected = new StringBuilder();
                expected.Append("<p>\r\nТекст <em>окруженный с двух сторон</em>  одинарными символами подчерка ");
                expected.Append("_Вот это_, не должно выделиться тегом <em>. ");
                expected.Append("<strong>Двумя символами</strong> — должен становиться жирным с помощью тега <strong>. ");
                expected.Append("Текст окруженный <code>одинарными _обратными_ кавычками</code> должен попадать в тег <code>\r\n</p>");
                Assert.AreEqual(result.ToString(), expected.ToString());
            }
        }
    }
}
