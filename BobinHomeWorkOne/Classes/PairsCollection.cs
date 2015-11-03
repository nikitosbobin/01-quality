using System;
using System.Collections.Generic;
using System.Linq;

namespace BobinHomeWorkOne.Classes
{
    class PairsCollection<TFirstType, TSecondType>
    {
        public PairsCollection()
        {
            leftList = new List<TFirstType>();
            rightList = new List<TSecondType>();
        }

        private List<TFirstType> leftList;
        private List<TSecondType> rightList;

        public bool Add(TFirstType item1, TSecondType item2)
        {
            if (item1 != null && item2 != null)
            {
                leftList.Add(item1);
                rightList.Add(item2);
                return true;
            }
            return false;
        }

        public int Length
        {
            get { return leftList.Count(); }
        }

        public Tuple<TFirstType, TSecondType> this[int index]
        {
            get
            {   if (index < leftList.Count())
                    return Tuple.Create(leftList[index], rightList[index]);
                return null;
            }
        }

        public Tuple<TFirstType, TSecondType> GetLastPair()
        {
            if (leftList.Count() > 0)
            {
                int lastIndex = leftList.Count() - 1;
                return Tuple.Create(leftList[lastIndex], rightList[lastIndex]);
            }
            return null;
        }
    }
}
