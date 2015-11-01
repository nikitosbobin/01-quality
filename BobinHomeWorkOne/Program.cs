using System;
using System.IO;

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
            //string input = @"Внутри _выделения \<em\> может быть __\<strong\>__ выделение_";
            //var g = new StringHandler(input);
            //Console.ReadKey();
        }
    }
}