using System;
using System.Collections.Generic;
using System.Text;

namespace BobinHomeWorkOne.Classes
{
    partial class Unit
    {
        public Unit(List<String> newUnit)
        {
            Lines = new List<Layout>();
            foreach (var t in newUnit)
                Lines.Add(new Layout(t));
        }

        public List<Layout> Lines { get; private set; }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<p>");
            foreach (var e in Lines)
                result.Append(e);
            result.AppendLine();
            result.Append("</p>");
            return result.ToString();
        }
    }
}
