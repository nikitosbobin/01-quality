using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace BobinHomeWorkOne
{
    public enum LayoutType
    {
        Null, Italic, Bold, Simple, Code
    }

    internal class Layout
    {
        public Layout(String origin)
        {
            type = LayoutType.Simple;
            inside = InputStringToListOfLayouts(origin);
        }

        public Layout(String origin, LayoutType type)
        {
            this.type = type;
            if (type == LayoutType.Simple || type == LayoutType.Code)
                this.origin = origin;
            else
                inside = InputStringToListOfLayouts(origin);
        }

        private static List<Layout> InputStringToListOfLayouts(String input)
        {
            var result = new List<Layout>();
            int start = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] == IGNORE)
                {
                    if (i < input.Length - 1 && (input[i + 1] == CODE || input[i + 1] == ITALIC))
                        input = input.Remove(i, 1);
                }
                else if (input[i] == CODE)
                {
                    var StringAfterCode = input.Substring(i + 1);
                    int index;
                    if ((index = StringAfterCode.IndexOf(CODE)) != -1)
                    {
                        result.Add(new Layout(input.Substring(start, i - start), LayoutType.Simple));
                        result.Add(new Layout(StringAfterCode.Substring(0, index), LayoutType.Code));
                        start = i = i + index + 2;
                    }
                }
                else if (input[i] == ITALIC)
                {
                    var thisTypeWord = GetNextUnderbar(input, i);
                    if (thisTypeWord.Item1 != LayoutType.Null)
                    {
                        result.Add(new Layout(input.Substring(start, i - start), LayoutType.Simple));
                        result.Add(new Layout(thisTypeWord.Item2, thisTypeWord.Item1));
                        i = i + thisTypeWord.Item2.Length + thisTypeWord.Item3;
                        start = i + 1;
                    }
                }
            }
            if (start < input.Length)
                result.Add(new Layout(input.Substring(start), LayoutType.Simple));
            return result;
        }

        public override String ToString()
        {
            switch (type)
            {
                case LayoutType.Simple: return inside == null ? origin : PrintInside();
                case LayoutType.Code: return String.Format("<code>{0}</code>", inside == null ? origin : PrintInside());
                case LayoutType.Bold: return String.Format("<strong>{0}</strong>", PrintInside());
                case LayoutType.Italic: return String.Format("<em>{0}</em>", PrintInside());
            }
            return "";
        }

        private String PrintInside()
        {
            StringBuilder result = new StringBuilder();
            foreach (var e in inside)
            {
                result.Append(e.ToString());
            }
            return result.ToString();
        }

        public List<Layout> inside;
        public LayoutType type;
        public String origin;
        private const char ITALIC = '_';
        private const char CODE = '`';
        private const char IGNORE = '\\';

        //написать тесты
        public static Tuple<LayoutType, String, int> GetNextUnderbar(String input, int startIndex)
        {
            int countStart = GetDownCount(input, true, startIndex);
            Tuple<int, int> result;
            if (countStart + startIndex < input.Length && input[countStart + startIndex] == ' ')
                return new Tuple<LayoutType, String, int>(LayoutType.Null, String.Empty, 0);
            result = FindLastMark(input.Substring(startIndex));
            int endIndex;
            if (result.Item1 != 0)
            {
                endIndex = result.Item2;
                if (countStart == 2 && result.Item1 >= 2)
                    return new Tuple<LayoutType, string, int>(LayoutType.Bold, input.Substring(startIndex + 2, endIndex - 3), result.Item1 + countStart);
                if ((countStart == 1 || countStart == 3) && result.Item1 >= 1)
                    return new Tuple<LayoutType, string, int>(LayoutType.Italic, input.Substring(startIndex + 1, endIndex - 1), result.Item1 + countStart);  
            }
            return new Tuple<LayoutType, string, int>(LayoutType.Null, String.Empty, 0);
        }

        //написать тесты
        public static Tuple<int, int> FindLastMark(String input)
        {
            var t = ExcludeCode(input);
            var layoutText = SplitOptions(t);
            if (string.IsNullOrEmpty(layoutText)) return new Tuple<int, int>(0, -1);
            return new Tuple<int, int>(GetDownCount(layoutText, false), t.IndexOf(layoutText) + layoutText.Length - 1);
        }

        //написать тесты
        public static String SplitOptions(String word)
        {
            String pattern;
            int countFirstMarks = GetDownCount(word, true);
            switch (countFirstMarks)
            {
                case 1:
                    pattern = @"_[^ ].*?[^ ]_( |$)";
                    break;
                case 2:
                    pattern = @"__[^ ].*?[^ ]__( |$)";
                    break;
                case 3:
                    pattern = @"___[^ ].*?[^ ]__( |$)";
                    break;
                default:
                    pattern = "";
                    break;
            }
            return Regex.Match(word, pattern).ToString().TrimEnd();
        }

        //обложен тестами
        public static String ExcludeCode(String word)
        {
            var e = Regex.Replace(word, @"\\`", o => new string('y', o.Length));
            while (e.Count(y => y == '`') > 1)
            {
                var b = Regex.Match(e, @"`.*?`").ToString();
                var t = e.IndexOf(b);
                String a1 = e.Substring(0, t);
                String a2 = new string('y', b.Length);
                String a3 = e.Substring(t + b.Length);
                e = (a1 + a2 + a3);
            }
            return e;
        }

        //обложен тестами
        public static int GetDownCount(String input, bool right, int index = 0)
        {
            int count = 0;
            if (right)
            {
                for (int i = index; i < input.Length; ++i)
                    if (input[i] == '_') count++;
                    else break;
            }
            else
            {
                for (int i = input.Length - 1; i >= 0; --i)
                    if (input[i] == '_') count++;
                    else break;
            }
            return count;
        }
    }

    public class Text
    {
        public Text(String[] inputText)
        {
            var tmpUnit = new List<String>();
            units = new List<Unit>();
            foreach (var e in inputText)
            {
                if (e == "")
                {
                    units.Add(new Unit(tmpUnit));
                    tmpUnit.Clear();
                }
                else
                    tmpUnit.Add(e);
            }
            units.Add(new Unit(tmpUnit));
        }

        private List<Unit> units;

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<html>");
            result.AppendLine("<body>");
            foreach (var e in units)
                result.AppendLine(e.ToString());
            result.AppendLine("</body>");
            result.AppendLine("</html>");
            return result.ToString();
        }

        private class Unit
        {
            public Unit(List<String> newUnit)
            {
                lines = new List<Layout>();
                foreach (var t in newUnit)
                    lines.Add(new Layout(t));
            }

            private List<Layout> lines;

            public override String ToString()
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("<p>");
                foreach (var e in lines)
                    result.AppendLine(e.ToString());
                result.Append("</p>");
                return result.ToString();
            }
        }
    }
}
