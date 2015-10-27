using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BobinHomeWorkOne
{
    public enum TagType
    {
        Null, Bold, Italic, Code, Collision, IgnoreNext, IgnoreBold, IgnoreItalic, IgnoreCode
    }

    class Program
    {
        static void Main(string[] args)
        {
            Line f = new Line("`_i_`");
            Console.ReadKey();
        }
    }
}
