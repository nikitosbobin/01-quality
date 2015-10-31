using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

namespace BobinHomeWorkOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.ReadAllLines(args[0]);
            var readyHtml = new Text(inputFile);
            Console.WriteLine(readyHtml.ToString());
            Console.ReadKey();
        }
    }
}

//если перед открывающимся тегом разметки написан обычный текст то происходит какой-то треш