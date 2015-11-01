using System;
using System.Collections.Generic;
using System.Text;

namespace BobinHomeWorkOne
{
    class Layout
    {
        public Layout(String origin)
        {
            StringHandler stringHandler = new StringHandler(origin);
            type = LayoutType.Simple;
            inside = stringHandler.Convert();
        }

        public Layout(String origin, LayoutType type)
        {
            StringHandler stringHandler = new StringHandler(origin);
            this.type = type;
            if (type == LayoutType.Simple || type == LayoutType.Code)
                this.origin = origin;
            else
                inside = stringHandler.Convert();
        }

        public override String ToString()
        {
            switch (type)
            {
                case LayoutType.Simple: return inside == null ? origin : PrintInside();
                case LayoutType.Code: return String.Format("<code>{0}</code>", inside == null ? origin : PrintInside());
                case LayoutType.Bold: return String.Format("<strong>{0}</strong>", PrintInside());
                case LayoutType.Italic: return String.Format("<em>{0}</em>", PrintInside());
            }
            return "";
        }

        private String PrintInside()
        {
            StringBuilder result = new StringBuilder();
            foreach (var e in inside)
                result.Append(e);
            return result.ToString();
        }

        public List<Layout> inside;
        public LayoutType type;
        public String origin;
    }
}
