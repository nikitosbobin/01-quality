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
                if (String.IsNullOrEmpty(e.TrimStart(' ')))
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

        public String[] GetResultText()
        {
            var result = new List<String>();
            result.Add("<html xml:lang=\"ru\" lang=\"ru\">");
            result.Add("<meta content=\"text/html; charset=UTF-8\">");
            result.Add("<body>");
            foreach (var e in units)
                result.Add(e.ToString());
            result.Add("</body>");
            result.Add("</html>");
            return result.ToArray();
        }
    }
}
