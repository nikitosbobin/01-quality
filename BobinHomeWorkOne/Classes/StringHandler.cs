using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BobinHomeWorkOne.Classes
{
    partial class StringHandler
    {
        public StringHandler(String inputData)
        {
            oneLevel = new PairsCollection<LayoutType, string>();
            _iterator = 0;
            Data = inputData;
        }

        public String Data { get; private set; }
        private int _iterator;
        private PairsCollection<LayoutType, String> oneLevel; 

        public List<Layout> Convert()
        {
            List<Layout> result = new List<Layout>();
            while (!IsIteratorEnd())
                MoveIterator();
            for (int i = 0; i < oneLevel.Length ;++i)
                result.Add(new Layout(oneLevel[i].Item2, oneLevel[i].Item1));
            return result;
        }

        public bool IsIteratorEnd()
        {
            return _iterator > Data.Length - 1;
        }

        public void MoveIterator()
        {
            for (int i = _iterator; i < Data.Length; ++i)
            {
                switch (Data[i])
                {
                    case '\\':
                        if (i < Data.Length - 1 && (Data[i + 1] == '*' ||Data[i + 1] == '`' || Data[i + 1] == '_'))
                        {
                            Data = Data.Remove(i, 1);
                            i++;
                        }
                        break;
                    case '`':
                        if (MoveIteratorToIgnoreCloser('`', i))
                            return;
                        break;
                    case '*':
                        if (MoveIteratorToIgnoreCloser('*', i))
                            return;
                        break;
                    case '_':
                        var thisTypeWord = GetNextUnderbar(i);
                        if (thisTypeWord.Type != LayoutType.Null)
                        {
                            if (i > _iterator)
                                oneLevel.Add(LayoutType.Simple, Data.Substring(_iterator, i - _iterator));
                            oneLevel.Add(thisTypeWord.Type, thisTypeWord.CleanedWord);
                            _iterator = i + thisTypeWord.LastIndex;
                            return;
                        }
                        i++;
                        break;
                }
            }
            if (Data.Length - 1 >= _iterator)
            {
                oneLevel.Add(LayoutType.Simple, Data.Substring(_iterator));
                _iterator = Data.Length;
            }
        }

        private bool MoveIteratorToIgnoreCloser(char ignoreChar, int index)
        {
            var thisType = ignoreChar == '*' ? LayoutType.Image : LayoutType.Code;
            var stringAfterImage = Data.Substring(index + 1);
            int imageEndIndex;
            if ((imageEndIndex = stringAfterImage.IndexOf(ignoreChar)) != -1)
            {
                if (index > _iterator)
                    oneLevel.Add(LayoutType.Simple, Data.Substring(_iterator, index - _iterator));
                oneLevel.Add(thisType, stringAfterImage.Substring(0, imageEndIndex));
                _iterator = index + imageEndIndex + 2;
                return true;
            }
            return false;
        }

        public FeaturedWord GetNextUnderbar(int startIndex)
        {
            int countStart = GetDownCount(Data, true, startIndex);
            Tuple<int, int> result;
            if (countStart + startIndex < Data.Length && Data[countStart + startIndex] == ' ')
                return new FeaturedWord();
            result = FindLastMark(Data.Substring(startIndex));
            if (result.Item1 != 0)
            {
                var endIndex = result.Item2;
                if (countStart == 2 && result.Item1 >= 2)
                    return new FeaturedWord(LayoutType.Bold, Data.Substring(startIndex + 2, endIndex - 3), result.Item2 + 1);
                return (countStart == 1 || countStart == 3) && result.Item1 >= 1
                    ? new FeaturedWord(LayoutType.Italic, Data.Substring(startIndex + 1, endIndex - 1), result.Item2 + 1)
                    : new FeaturedWord();
            }
            return new FeaturedWord();
        }

        private static String SplitWithMoreAccuracy(String input)
        {
            String pattern = @".+?[^ \\_]_( |$|\.|,|;)";
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

        public Tuple<int, int> FindLastMark(String input)
        {
            var t = SimplifyCodeLayout(input).TrimEnd(';', '.', ' ', ',');
            var layoutText = SplitOptions(t);
            if (String.IsNullOrEmpty(layoutText)) return new Tuple<int, int>(0, -1);
            return Tuple.Create(GetDownCount(layoutText, false),
                t.IndexOf(layoutText, StringComparison.Ordinal) + layoutText.Length - 1);
        }

        public static String SplitOptions(String word)
        {
            String pattern;
            int countFirstMarks = GetDownCount(word, true);
            switch (countFirstMarks)
            {
                case 1: return SplitWithMoreAccuracy(word).TrimEnd();
                case 2:
                    pattern = @"__.*?[^ \\]__( |$|\.|,|;)";
                    break;
                case 3:
                    pattern = @"___.*?[^ \\]__( |$|\.|,|;)";
                    break;
                default:
                    pattern = "";
                    break;
            }
            return Regex.Match(word, pattern).ToString().TrimEnd();
        }
        
        private String SimplifyCodeLayout(String input)
        {
            var tempBuilder = new StringBuilder();
            int openerIndex = 0;
            String inputWithoutIgnore = Regex.Replace(input, @"\\`|\\\*", ignoreBlock => new String('y', ignoreBlock.Length));
            while ((openerIndex = FindFirstOpener(inputWithoutIgnore, openerIndex)) != -1)
            {
                var mainOpener = input[openerIndex];
                while (inputWithoutIgnore.Count(y => y == mainOpener) > 1)
                {
                    var oneBlockInCodeTag = Regex.Match(inputWithoutIgnore, '\\' + mainOpener.ToString() + ".*?" + '\\' + mainOpener).ToString();
                    var indexOfStartCodeTag = inputWithoutIgnore.IndexOf(oneBlockInCodeTag, StringComparison.Ordinal);
                    tempBuilder.Append(inputWithoutIgnore.Substring(0, indexOfStartCodeTag));
                    tempBuilder.Append(new String('y', oneBlockInCodeTag.Length));
                    tempBuilder.Append(inputWithoutIgnore.Substring(indexOfStartCodeTag + oneBlockInCodeTag.Length));
                    inputWithoutIgnore = tempBuilder.ToString();
                    tempBuilder.Clear();
                    openerIndex += oneBlockInCodeTag.Length - 1;
                }
                openerIndex++;
            }
            return inputWithoutIgnore;
        }

        private static int FindFirstOpener(String input, int start)
        {
            var tmp = input.Substring(start);
            var firstOpener = Regex.Match(tmp, @"`|\*").ToString();
            var index = input.IndexOf(firstOpener, StringComparison.Ordinal);
            return !String.IsNullOrEmpty(firstOpener) ? index : -1;
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
                    else break;
            }
            return count;
        }
    }
}
