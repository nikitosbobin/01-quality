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
    enum wordsSetType
    {
        Simple, Bold, Italic, Code
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var inputFile = File.ReadAllLines(args[0]);
            //var text = new Text(inputFile);
            var a = "ехал _Грека _тёмную_ через_ реку";
            Console.WriteLine(new Line(a).Content);
            Console.ReadKey();
        }
    }
}
