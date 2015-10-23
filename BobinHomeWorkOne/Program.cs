using System;
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
        Simple, Bold, Italic
    }

    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.ReadAllLines(args[0]);
            Console.WriteLine("helloworld");
        }
    }

    class Text
    {
        Text(string[] inputText)
        {
            var tmpUnit = new List<string>();
            foreach (var e in inputText)
            {
                tmpUnit.Add(e);
                if (e == "\n")
                {
                    units.Add(new Unit(tmpUnit));
                    tmpUnit.Clear();
                }
            }
        }

        private List<Unit> units;

        private class Unit
        {
            public Unit(List<string> newUnit)
            {
                wordsSets = new List<WordsSet>();
            }

            private List<WordsSet> wordsSets; 

            private class WordsSet
            {
                public WordsSet(string content, params wordsSetType[] types)
                {
                    this.content = content;
                    this.types = types;
                }

                private wordsSetType[] types;
                private 
                private string content;

                public string Content {
                    get { return content; }
                    set { content = value; }
                }

                public wordsSetType[] Types
                {
                    get { return types; }
                    set { types = value; }
                }
            }
        }

        /// <summary>
        /// Print your text into html file.
        /// </summary>
        /// <param name="name">Name of out file, without ".html"</param>
        /// <returns></returns>
        public bool ToHtml(string name)
        {
            try
            {
                //to do
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
