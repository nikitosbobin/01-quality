using System;
using System.Collections.Generic;
using System.Text;

namespace BobinHomeWorkOne
{
    public class Text
    {
        public Text(String[] inputText)
        {
            var tmpUnit = new List<String>();
            units = new List<Unit>();
            foreach (var e in inputText)
            {
                if (e == "")
                {
                    units.Add(new Unit(tmpUnit));
                    tmpUnit.Clear();
                }
                else
                    tmpUnit.Add(e);
            }
            units.Add(new Unit(tmpUnit));
        }

        private List<Unit> units;

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<html>");
            result.AppendLine("<body>");
            foreach (var e in units)
                result.AppendLine(e.ToString());
            result.AppendLine("</body>");
            result.AppendLine("</html>");
            return result.ToString();
        }
    }
}
