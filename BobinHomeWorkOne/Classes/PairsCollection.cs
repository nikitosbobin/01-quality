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

        public void Add(TFirstType item1, TSecondType item2)
        {
            if (item1 != null) leftList.Add(item1);
            if (item2 != null) rightList.Add(item2);
        }

        public int Length
        {
            get { return leftList.Count(); }
        }

        public Tuple<TFirstType, TSecondType> this[int index]
        {
            get
            {
                return Tuple.Create(leftList[index], rightList[index]);
            }
        }
    }
}
