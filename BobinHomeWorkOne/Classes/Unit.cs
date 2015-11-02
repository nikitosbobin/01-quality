using System;
using System.Collections.Generic;
using System.Text;

namespace BobinHomeWorkOne
{
    class Unit
    {
        public Unit(List<String> newUnit)
        {
            lines = new List<Layout>();
            foreach (var t in newUnit)
                lines.Add(new Layout(t));
        }

        private List<Layout> lines;

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<p>");
            foreach (var e in lines)
                result.Append(e.ToString());
            result.AppendLine();
            result.Append("</p>");
            return result.ToString();
        }
    }
}
