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
            if (input == null) return false;
            return input.CleanedWord == CleanedWord && input.LastIndex == LastIndex && input.Type == Type;
        }

        protected bool Equals(FeaturedWord other)
        {
            return Type == other.Type && string.Equals(CleanedWord, other.CleanedWord) && LastIndex == other.LastIndex;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Type;
                hashCode = (hashCode*397) ^ (CleanedWord != null ? CleanedWord.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ LastIndex;
                return hashCode;
            }
        }
    }
}
