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
    public enum wordsSetType
    {
        SimpleOpen, SimpleClose, BoldOpen, BoldClose, ItalicOpen, ItalicSlose, 
        CodeOpen, CodeClose, Collision, IgnoreOpen, IgnoreClose
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var inputFile = File.ReadAllLines(args[0]);
            //var text = new Text(inputFile);

            Console.WriteLine(Line.GetOpeners("`___hh").Length);
            Console.ReadKey();
        }
    }
}
