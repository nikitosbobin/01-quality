using System;
using System.IO;
using System.Text;

namespace BobinHomeWorkOne
{
    class Program
    {
        static void Main(String[] args)
        {
            var inputFile = File.ReadAllLines(args[0], Encoding.Default);
            var readyHtml = new Text(inputFile);
            File.WriteAllLines("out.html", readyHtml.GetResultText(), Encoding.UTF8);
        }
    }
}