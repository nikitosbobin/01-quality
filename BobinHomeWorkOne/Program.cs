using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BobinHomeWorkOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.ReadAllLines(args[0]);
            var readyHtml = new Text(inputFile);
            var a = File.Create("out.html");
            StreamWriter o = new StreamWriter(a);
            o.WriteLine(readyHtml.ToString());
            o.Close();
            //Console.ReadKey();
        }
    }
}