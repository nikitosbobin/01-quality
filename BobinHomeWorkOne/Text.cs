using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobinHomeWorkOne
{
    public class Line
    {
        public static int GetDownCount(string word, int start)
        {
            int count = 0;
            for (int i = start; i < word.Length; ++i)
                if (word[i] == '_') count++;
                else break;
            return count;
        }

        public Line(string line)
        {
            content = line;
            listOfTypes = new List<KeyValuePair<TagType, int>>();
            var stack = new Stack<KeyValuePair<TagType, int>>();
            for (int i = 0; i < line.Length; ++i)
            {
                var tmp = GetTagType(line, ref i);
                if (tmp.Key != TagType.Null)
                {
                    if (stack.Count() != 0)
                    {
                        if (stack.Peek().Key == tmp.Key)
                        {
                            listOfTypes.Add(stack.Pop());
                            listOfTypes.Add(tmp);
                        }
                        else
                        {
                            if (tmp.Key == TagType.IgnoreNext)
                            {
                                int next = i + 1;
                                var t = GetTagType(line, ref next);
                                listOfTypes.Add(new KeyValuePair<TagType, int>(TransformIgnore(t.Key), t.Value));
                                i = next;
                            }
                            else if ((tmp.Key == TagType.Italic || tmp.Key == TagType.Bold) && stack.Peek().Key == TagType.Collision)
                            {
                                var temp = stack.Pop();
                                listOfTypes.Add(new KeyValuePair<TagType, int>(TagType.Italic, temp.Value));
                                if (tmp.Key == TagType.Italic)
                                {
                                    listOfTypes.Add(tmp);
                                }
                                else
                                {
                                    listOfTypes.Add(new KeyValuePair<TagType, int>(TagType.Italic, tmp.Value + 1));
                                }
                            }
                            else stack.Push(tmp);
                        }
                    }
                    else
                    {
                        if (tmp.Key == TagType.IgnoreNext)
                        {
                            int next = i + 1;
                            var t = GetTagType(line, ref next);
                            listOfTypes.Add(new KeyValuePair<TagType, int>(TransformIgnore(t.Key), t.Value - 1));
                            i = next;
                        }
                        else stack.Push(tmp);
                    }
                }
            }
        }
        //где-то перепрыгивает
        //не понимает пробелы
        private static TagType TransformIgnore(TagType ignored)
        {
            switch (ignored)
            {
                case TagType.Bold:
                    return TagType.IgnoreBold;
                case TagType.Code:
                    return TagType.IgnoreCode;
                case TagType.Italic:
                    return TagType.IgnoreItalic;
                case TagType.Collision:
                    return TagType.IgnoreItalic;
                default:
                    return TagType.Null;
            }
        }

        public static bool IsTag(string line, int index)
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

        public static KeyValuePair<TagType, int> GetTagType(string line, ref int index)
        {
            if ((line.Length <= index + 1) ||
                !IsTag(line, index)) return new KeyValuePair<TagType, int>(TagType.Null, -1);
            switch (line[index])
            {
                case '`':
                    return new KeyValuePair<TagType, int>(TagType.Code, index);
                case '\\':
                    return new KeyValuePair<TagType, int>(IsTag(line, index + 1) ? TagType.IgnoreNext : TagType.Null,
                        index);
            }
            var countMd = GetDownCount(line, index);
            var oldI = index;
            index += countMd;
            switch (countMd)
            {
                case 1:
                    return new KeyValuePair<TagType, int>(TagType.Italic, oldI);
                case 2:
                    return new KeyValuePair<TagType, int>(TagType.Bold, oldI);
                case 3:
                    return new KeyValuePair<TagType, int>(TagType.Collision, oldI);
                default:
                    return new KeyValuePair<TagType, int>(TagType.Null, -1);
            }
        }

        private List<KeyValuePair<TagType, int>> listOfTypes;
        private string content;
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
