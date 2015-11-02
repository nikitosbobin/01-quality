using System;
using System.IO;
using System.Text;

namespace BobinHomeWorkOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.ReadAllLines(args[0], Encoding.Default);
            var readyHtml = new Text(inputFile);
            var a = File.Create("out.html");
            StreamWriter o = new StreamWriter(a);
            o.WriteLine(readyHtml.ToString());
            o.Close();
            //string a = "_рпаврпыоавлр_.";
            //var tttt = new Layout(a);
        }
    }
}