using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobinHomeWorkOne
{
    public class Line
    {
        private static Dictionary<string, string> htmlTags;

        private static void InitTags()
        {
            htmlTags = new Dictionary<string, string>();
            htmlTags["_"] = "em>";
            htmlTags["__"] = "strong>";
            htmlTags["`"] = "code>";
        }

        private static string GetOpener(string word)
        {
            if (word.IndexOf("_") == 0) return "_";
            if (word.IndexOf("`") == 0) return "`";
            if (word.IndexOf("__") == 0) return "__";
            return "";
        }

        public Line(string content)
        {
            InitTags();
            var tempWords = content.Split(' ');
            var stackTags = new Stack<string>();
            var result = new StringBuilder();
            string tmpTag;
            for (var i = 0; i < tempWords.Length; ++i)
            {
                if ((tmpTag = GetOpener(tempWords[i])) != "")
                {
                    stackTags.Push(tmpTag);
                    result.Append("<");
                    result.Append(htmlTags[tmpTag]);
                    result.Append(tempWords[i].Substring(tmpTag.Length));
                }
                else if (stackTags.Count() != 0 && (tmpTag = stackTags.Peek()) ==
                         tempWords[i].Substring(tempWords[i].Length - stackTags.Peek().Length))
                {
                    stackTags.Pop();
                    result.Append(tempWords[i].Substring(0, tempWords[i].Length - tmpTag.Length));
                    result.Append("</");
                    result.Append(htmlTags[tmpTag]);
                }
                else result.Append(tempWords[i]);
                result.Append(i == (tempWords.Length - 1) ? "\n" : " ");
            }
            this.content = result.ToString();
        }
        private string content;
        public string Content {
            get { return content; }
            private set { }
        }
    }

    public class Text
    {
        public Text(string[] inputText)
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

        public class Unit
        {
            public Unit(List<string> newUnit)
            {
                lines = new List<Line>();
                foreach (var t in newUnit)
                    lines.Add(new Line(t));
            }

            private List<Line> lines;
        }

        public static int abc(int a)
        {
            return a;
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
