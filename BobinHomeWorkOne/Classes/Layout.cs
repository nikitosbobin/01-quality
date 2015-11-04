using System;
using System.Collections.Generic;
using System.Text;

namespace BobinHomeWorkOne.Classes
{
    class Layout
    {
        public Layout(String origin)
        {
            var stringHandler = new StringHandler(origin);
            type = LayoutType.Simple;
            insideLayouts = stringHandler.Convert();
        }

        public Layout(String origin, LayoutType type)
        {
            var stringHandler = new StringHandler(origin);
            this.type = type;
            if (type == LayoutType.Simple || type == LayoutType.Code || type == LayoutType.Image)
                this.origin = origin;
            else
                insideLayouts = stringHandler.Convert();
        }

        public override String ToString()
        {
            switch (type)
            {
                case LayoutType.Simple: return insideLayouts == null ? origin : InsideLayoutsToString();
                case LayoutType.Code: return String.Format("<code>{0}</code>", origin);
                case LayoutType.Image: return String.Format("<image>{0}</image>", origin);
                case LayoutType.Bold: return String.Format("<strong>{0}</strong>", InsideLayoutsToString());
                case LayoutType.Italic: return String.Format("<em>{0}</em>", InsideLayoutsToString());
            }
            return "";
        }

        private String InsideLayoutsToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var e in insideLayouts)
                result.Append(e);
            return result.ToString();
        }

        private List<Layout> insideLayouts;
        private LayoutType type;
        private String origin;
    }
}
