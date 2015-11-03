using System;

namespace BobinHomeWorkOne.Classes
{
    class FeaturedWord
    {
        public FeaturedWord()
        {
            Type = LayoutType.Null;
            CleanedWord = String.Empty;
            EdgesCount = 0;
        }

        public FeaturedWord(LayoutType type, String cleanedWord, int edgesCount)
        {
            Type = type;
            CleanedWord = cleanedWord;
            EdgesCount = edgesCount;
        }

        public LayoutType Type { get; private set; }
        public String CleanedWord { get; private set; }
        public int EdgesCount { get; private set; }
    }
}
