using System;
using NUnit.Framework;

namespace BobinHomeWorkOne
{
    [TestFixture]
    class Text_Test
    {
        [TestCase("ехал _Грека через_ реку", Result = "ехал <em>Грека через</em> реку\n", TestName = "one em layout")]
        [TestCase("ехал `Грека через` реку", Result = "ехал <code>Грека через</code> реку\n", TestName = "one code layout")]
        public string testOne(string input)
        {
            return (new Line(input)).Content;
        }
    }
}
