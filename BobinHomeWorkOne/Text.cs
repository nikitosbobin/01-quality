using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobinHomeWorkOne
{
    public class Line
    {
        public static wordsSetType[] GetOpeners(string word)
        {
            var result = new List<wordsSetType>();
            int end = -1;
            for (int i = 0; i < word.Length; ++i)
                if (word[i] != '`' && word[i] != '\\' && word[i] != '_') { end = i - 1; break; }
            if (end == -1 || end + 1 == word.Length) return null;
            for (int i = 0; i <= end; ++i)
            {
                switch (word[i])
                {
                    case '\\': result.Add(wordsSetType.IgnoreOpen);
                        continue;
                    case '`': result.Add(wordsSetType.CodeOpen); 
                        continue;
                    default: HandleDownMarks(result, word, ref i);
                        continue;
                }
            }
            return result.ToArray();
        }

        private static void HandleDownMarks(List<wordsSetType> result, string word, ref int i)
        {
            var down = GetDownCount(word, i);
                switch (down)
                {
                    case 1: result.Add(wordsSetType.ItalicOpen); break;
                    case 2: i++; result.Add(wordsSetType.BoldOpen); break;
                    default: i += down - 1; result.Add(wordsSetType.Collision); break;
                }
        }

        public static int GetDownCount(string word, int start)
        {
            int count = 0;
            for (int i = start; i < word.Length; ++i)
                if (word[i] == '_') count++;
                else return count;
            return 0;
        }

        public Line(string input)
        {
            var stack = new Stack<KeyValuePair<wordsSetType, int>>();
            var tempWords = input.Split(' ');
            for (var i = 0; i < tempWords.Length; ++i)
            {

            }
        }

        //public Line(string content)
        //{
        //    InitTags();
        //    var tempWords = content.Split(' ');
        //    var stackTags = new Stack<string>();
        //    var result = new StringBuilder();
        //    string tmpTag;
        //    for (var i = 0; i < tempWords.Length; ++i)
        //    {
        //        if ((tmpTag = GetOpener(tempWords[i])) != "")
        //        {
        //            stackTags.Push(tmpTag);
        //            result.Append("<");
        //            result.Append(htmlTags[tmpTag]);
        //            result.Append(tempWords[i].Substring(tmpTag.Length));
        //        }
        //        else if (stackTags.Count() != 0 && (tmpTag = stackTags.Peek()) ==
        //                 tempWords[i].Substring(tempWords[i].Length - stackTags.Peek().Length))
        //        {
        //            stackTags.Pop();
        //            result.Append(tempWords[i].Substring(0, tempWords[i].Length - tmpTag.Length));
        //            result.Append("</");
        //            result.Append(htmlTags[tmpTag]);
        //        }
        //        else result.Append(tempWords[i]);
        //        result.Append(i == (tempWords.Length - 1) ? "\n" : " ");
        //    }
        //    this.content = result.ToString();
        //}
        private string content;
        public string Content
        {
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
