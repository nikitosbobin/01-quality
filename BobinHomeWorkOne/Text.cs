using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BobinHomeWorkOne
{
    public enum LayoutType
    {
        Null, Simple, Bold, Italic, Code, Collision, IgnoreNext, IgnoreBold, IgnoreItalic, IgnoreCode
    }

    public class Layout
    {
        public Layout(string origin)
        {
            type = LayoutType.Simple;
            inside = InputStringToListOfLayouts(origin);
        }

        public Layout(string origin, LayoutType type)
        {
            this.type = type;
            if (type == LayoutType.Simple || type == LayoutType.Code)
                this.origin = origin;
            else
                inside = InputStringToListOfLayouts(origin);
        }

        private static List<Layout> InputStringToListOfLayouts(string input)
        {
            var result = new List<Layout>();
            char thisTagChar = EMPTY_CHAR;
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
                    var stringAfterCode = input.Substring(i + 1);
                    int index;
                    if ((index = stringAfterCode.IndexOf(CODE)) != -1)
                    {
                        if (thisTagChar == EMPTY_CHAR)
                            result.Add(new Layout(input.Substring(start, i - start), LayoutType.Simple));
                        result.Add(new Layout(stringAfterCode.Substring(0, index), LayoutType.Code));
                        start = i = i + index + 2;
                    }
                }
                else if (input[i] == ITALIC)
                {
                    var tmpResult = GetEndOfUnderscore(input, i);
                    var item1 = tmpResult.Item1;
                    var item2 = tmpResult.Item2;
                    var item3 = tmpResult.Item3;
                    if (item1 == item2 && item2 == 2)
                    {
                        result.Add(new Layout(input.Substring(i + 2, item3 - i - 2), LayoutType.Bold));
                        i = item3;
                    }
                    else if (item1 != 0 && item2 != 0)
                    {
                        result.Add(new Layout(input.Substring(i + 1, item3 - i - 1), LayoutType.Bold));
                        i = item3;
                    }
                }
            }
            if (start != input.Length)
                result.Add(new Layout(input.Substring(start), LayoutType.Simple));
            return result;
        }

        public override string ToString()
        {
            switch (type)
            {
                case LayoutType.Simple: return inside == null ? origin : PrintInside();
                case LayoutType.Code: return string.Format("<code>{0}</code>", inside == null ? origin : PrintInside());
                case LayoutType.Bold: return string.Format("<strong>{0}</strong>", PrintInside());
                case LayoutType.Italic: return string.Format("<em>{0}</em>", PrintInside());
            }
            return "";
        }

        private string PrintInside()
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
        public string origin;
        private const char EMPTY_CHAR = 'h';
        private const char ITALIC = '_';
        private const char BOLD = 'B';
        private const char CODE = '`';
        private const char IGNORE = '\\';

        //item1 - это количество подчеркиваний в начале
        //item2 - это количество подчеркиваний в конце
        //item3 - это индекс последнего подчёркивания
        //выходные данные полностью соответствуют требованиям программы
        public static Tuple<int, int, int> GetEndOfUnderscore(string input, int index)
        {
            int countStart = GetDownCount(input, index);
            if (input[countStart + index] == ' ' || countStart > 3)
                return new Tuple<int, int, int>(countStart, 0, -1);
            int countEnd = 0;
            var temmp = input.Substring(countStart + index);
            int endIndex = -1;
            int i = countStart + index - 1;
            while ((i = input.Substring(i + 1).IndexOf(" ")) != -1)
            {
                if (input[i - 1] == '_')
                {
                    countEnd = GetDownCountLeft(input, i - 1);

                }
                temmp = temmp.Substring(i + 1);
            }
            return new Tuple<int, int, int>(countStart, countEnd, endIndex);
        }

        private static int GetDownCount(string input, int index)
        {
            int count = 0;
            for (int i = index; i < input.Length; ++i)
                if (input[i] == '_') count++;
                else break;
            return count;
        }

        private static int GetDownCountLeft(string input, int index)
        {
            int count = 0;
            for (int i = input.Length - 1; i >= index; --i)
                if (input[i] == '_') count++;
                else break;
            return count;
        }

        private static bool IsTag(string line, int index)
        {
            try
            {
                if (line[index] == '`' || line[index] == '_' || line[index] == '\\') return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
    }

    //public class Line
    //{
    //    public static int GetDownCount(string word, int start)
    //    {
    //        int count = 0;
    //        for (int i = start; i < word.Length; ++i)
    //            if (word[i] == '_') count++;
    //            else break;
    //        return count;
    //    }

    //    public Line(string line)
    //    {
    //        content = line;
    //        listOfTypes = new List<KeyValuePair<TagType, int>>();
    //        var stack = new Stack<KeyValuePair<TagType, int>>();
    //        for (int i = 0; i < line.Length; ++i)
    //        {
    //            var tmp = GetTagType(line, ref i);
    //            if (tmp.Key != TagType.Null)
    //            {
    //                if (stack.Count() != 0)
    //                {
    //                    if (stack.Peek().Key == tmp.Key)
    //                    {
    //                        listOfTypes.Add(stack.Pop());
    //                        listOfTypes.Add(tmp);
    //                    }
    //                    else
    //                    {
    //                        if (tmp.Key == TagType.IgnoreNext)
    //                        {
    //                            int next = i + 1;
    //                            var t = GetTagType(line, ref next);
    //                            listOfTypes.Add(new KeyValuePair<TagType, int>(TransformIgnore(t.Key), t.Value));
    //                            i = next;
    //                        }
    //                        else if ((tmp.Key == TagType.Italic || tmp.Key == TagType.Bold) && stack.Peek().Key == TagType.Collision)
    //                        {
    //                            var temp = stack.Pop();
    //                            listOfTypes.Add(new KeyValuePair<TagType, int>(TagType.Italic, temp.Value));
    //                            if (tmp.Key == TagType.Italic)
    //                            {
    //                                listOfTypes.Add(tmp);
    //                            }
    //                            else
    //                            {
    //                                listOfTypes.Add(new KeyValuePair<TagType, int>(TagType.Italic, tmp.Value + 1));
    //                            }
    //                        }
    //                        else stack.Push(tmp);
    //                    }
    //                }
    //                else
    //                {
    //                    if (tmp.Key == TagType.IgnoreNext)
    //                    {
    //                        int next = i + 1;
    //                        var t = GetTagType(line, ref next);
    //                        listOfTypes.Add(new KeyValuePair<TagType, int>(TransformIgnore(t.Key), t.Value - 1));
    //                        i = next;
    //                    }
    //                    else stack.Push(tmp);
    //                }
    //            }
    //        }
    //    }

    //    private static TagType TransformIgnore(TagType ignored)
    //    {
    //        switch (ignored)
    //        {
    //            case TagType.Bold:
    //                return TagType.IgnoreBold;
    //            case TagType.Code:
    //                return TagType.IgnoreCode;
    //            case TagType.Italic:
    //                return TagType.IgnoreItalic;
    //            case TagType.Collision:
    //                return TagType.IgnoreItalic;
    //            default:
    //                return TagType.Null;
    //        }
    //    }

    //    public static bool IsTag(string line, int index)
    //    {
    //        try
    //        {
    //            if (line[index] == '`' || line[index] == '_' || line[index] == '\\') return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        return false;
    //    }

    //    public static KeyValuePair<TagType, int> GetTagType(string line, ref int index)
    //    {
    //        if ((line.Length <= index + 1) ||
    //            !IsTag(line, index)) return new KeyValuePair<TagType, int>(TagType.Null, -1);
    //        switch (line[index])
    //        {
    //            case '`':
    //                return new KeyValuePair<TagType, int>(TagType.Code, index);
    //            case '\\':
    //                return new KeyValuePair<TagType, int>(IsTag(line, index + 1) ? TagType.IgnoreNext : TagType.Null,
    //                    index);
    //        }
    //        var countMd = GetDownCount(line, index);
    //        var oldI = index;
    //        index += countMd;
    //        switch (countMd)
    //        {
    //            case 1:
    //                return new KeyValuePair<TagType, int>(TagType.Italic, oldI);
    //            case 2:
    //                return new KeyValuePair<TagType, int>(TagType.Bold, oldI);
    //            case 3:
    //                return new KeyValuePair<TagType, int>(TagType.Collision, oldI);
    //            default:
    //                return new KeyValuePair<TagType, int>(TagType.Null, -1);
    //        }
    //    }

    //    private List<KeyValuePair<TagType, int>> listOfTypes;
    //    private string content;
    //}

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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<p>");
            foreach (var e in units)
            {
                result.AppendLine(e.ToString());
            }
            result.AppendLine("</p>");
            return result.ToString();
        }

        public class Unit
        {
            public Unit(List<string> newUnit)
            {
                lines = new List<Layout>();
                foreach (var t in newUnit)
                    lines.Add(new Layout(t));
            }

            private List<Layout> lines;

            public override string ToString()
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("<p>");
                foreach (var e in lines)
                {
                    result.AppendLine(e.ToString());
                }
                result.AppendLine("</p>");
                return result.ToString();
            }
        }
    }
}
