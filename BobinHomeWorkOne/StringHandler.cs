using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BobinHomeWorkOne
{
    class StringHandler
    {
        public StringHandler(String inputData)
        {
            oneLevel = new List<KeyValuePair<LayoutType, string>>();
            iterator = 0;
            data = inputData;
            while (!IsIteratorEnd())
                MoveIterator();
        }

        private String data;
        public String Data
        {
            get { return data; }
            private set { }
        }
        private int iterator;
        private List<KeyValuePair<LayoutType, String>> oneLevel;

        public List<Layout> Convert()
        {
            List<Layout> result = new List<Layout>();
            foreach (var item in oneLevel)
                result.Add(new Layout(item.Value, item.Key));
            return result;  
        }

        public bool IsIteratorEnd()
        {
            return iterator > data.Length - 1;
        }

        private bool MoveIterator()
        {
            for (int i = iterator; i < data.Length; ++i)
            {
                if (data[i] == '\\')
                {
                    if (i < data.Length - 1 && (data[i + 1] == '`' || data[i + 1] == '_'))
                    {
                        iterator = i + 2;
                        return true;
                    }
                    i++;
                }
                if (data[i] == '`')
                {
                    var StringAfterCode = data.Substring(i + 1);
                    int index;
                    if ((index = StringAfterCode.IndexOf('`')) != -1)
                    {
                        if (i > iterator)
                            oneLevel.Add(new KeyValuePair<LayoutType, string>(LayoutType.Simple, data.Substring(iterator, i - iterator)));
                        oneLevel.Add(new KeyValuePair<LayoutType, string>(LayoutType.Code, StringAfterCode.Substring(0, index)));
                        iterator = i + index + 2;
                        return true;
                    }
                }
                if (data[i] == '_')
                {
                    var thisTypeWord = GetNextUnderbar(i);
                    if (thisTypeWord.Item1 != LayoutType.Null)
                    {
                        if (i > iterator)
                            oneLevel.Add(new KeyValuePair<LayoutType, string>(LayoutType.Simple, data.Substring(iterator, i - iterator)));
                        oneLevel.Add(new KeyValuePair<LayoutType, string>(thisTypeWord.Item1, thisTypeWord.Item2));
                        iterator = i + thisTypeWord.Item2.Length + thisTypeWord.Item3;
                        return true;
                    }
                    i++;
                }
            }
            if (data.Length - 1 >= iterator)
            {
                oneLevel.Add(new KeyValuePair<LayoutType, string>(LayoutType.Simple, data.Substring(iterator)));
                iterator = data.Length;
                return true;
            }
            return false;
        }

        public Tuple<LayoutType, String, int> GetNextUnderbar(int startIndex)
        {
            int countStart = GetDownCount(data, true, startIndex);
            Tuple<int, int> result;
            if (countStart + startIndex < data.Length && data[countStart + startIndex] == ' ')
                return new Tuple<LayoutType, String, int>(LayoutType.Null, String.Empty, 0);
            result = FindLastMark(data.Substring(startIndex));
            if (result.Item1 != 0)
            {
                var endIndex = result.Item2;
                if (countStart == 2 && result.Item1 >= 2)
                    return new Tuple<LayoutType, string, int>(LayoutType.Bold, data.Substring(startIndex + 2, endIndex - 3), result.Item1 + countStart);
                if ((countStart == 1 || countStart == 3) && result.Item1 >= 1)
                    return new Tuple<LayoutType, string, int>(LayoutType.Italic, data.Substring(startIndex + 1, endIndex - 1), result.Item1 + countStart);
            }
            return new Tuple<LayoutType, string, int>(LayoutType.Null, String.Empty, 0);
        }

        private Tuple<int, int> FindLastMark(String input)
        {
            var t = SimplifyCodeLayout(input);
            var layoutText = SplitOptions(t);
            if (string.IsNullOrEmpty(layoutText)) return new Tuple<int, int>(0, -1);
            return new Tuple<int, int>(GetDownCount(layoutText, false), t.IndexOf(layoutText) + layoutText.Length - 1);
        }

        public static String SplitOptions(String word)
        {
            String pattern;
            int countFirstMarks = GetDownCount(word, true);
            if (countFirstMarks == 1)
                return SplitWithMoreAccuracy(word).TrimEnd();
            switch (countFirstMarks)
            {
                case 2:
                    pattern = @"__.*?[^ \\]__( |$)";
                    break;
                case 3:
                    pattern = @"___.*?[^ \\]__( |$)";
                    break;
                default:
                    pattern = "";
                    break;
            }
            return Regex.Match(word, pattern).ToString().TrimEnd();
        }

        private static String SplitWithMoreAccuracy(String input)
        {
            String pattern = @".+[^ \\]_( |$)";
            int endOfPattern = GetResidueOpenerIndex(input);
            if (endOfPattern == -1) return Regex.Match(input, pattern).ToString();
            return Regex.Match(input.Substring(0, endOfPattern), pattern).ToString();
        }

        private static int GetResidueOpenerIndex(String residue)
        {
            String findedSubstring = Regex.Match(residue, @" _[^_]| ___").ToString();
            if (String.IsNullOrEmpty(findedSubstring)) return -1;
            return residue.IndexOf(findedSubstring, StringComparison.Ordinal);
        }

        private String SimplifyCodeLayout(String input)
        {
            var tempBuilder = new StringBuilder();
            var inputWithoutIgnore = Regex.Replace(input, @"\\`", ignoreBlock => new string('y', ignoreBlock.Length));
            while (inputWithoutIgnore.Count(y => y == '`') > 1)
            {
                var oneBlockInCodeTag = Regex.Match(inputWithoutIgnore, @"`.*?`").ToString();
                var indexOfStartCodeTag = inputWithoutIgnore.IndexOf(oneBlockInCodeTag);
                tempBuilder.Append(inputWithoutIgnore.Substring(0, indexOfStartCodeTag));
                tempBuilder.Append(new string('y', oneBlockInCodeTag.Length));
                tempBuilder.Append(inputWithoutIgnore.Substring(indexOfStartCodeTag + oneBlockInCodeTag.Length));
                inputWithoutIgnore = tempBuilder.ToString();
                tempBuilder.Clear();
            }
            return inputWithoutIgnore;
        }

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
                    else if (input[i] != ' ') break;
            }
            return count;
        }
    }
}
