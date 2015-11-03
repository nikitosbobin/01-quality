using System;

namespace BobinHomeWorkOne.Classes
{
    class FeaturedWord
    {
        public FeaturedWord()
        {
            Type = LayoutType.Null;
            CleanedWord = String.Empty;
            LastIndex = 0;
        }

        public FeaturedWord(LayoutType type, String cleanedWord, int lastIndex)
        {
            Type = type;
            CleanedWord = cleanedWord;
            LastIndex = lastIndex;
        }

        public LayoutType Type { get; private set; }
        public String CleanedWord { get; private set; }
        public int LastIndex { get; private set; }

        public override bool Equals(object obj)
        {
            var input = obj as FeaturedWord;
            return input.CleanedWord == CleanedWord && input.LastIndex == LastIndex && input.Type == Type;
        }
    }
}
