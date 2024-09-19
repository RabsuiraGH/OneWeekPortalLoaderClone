using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Utility.DebugTool
{
    public static class DebugColorOptions
    {
        public enum HtmlColor
        {
            Bool,
            Red,
            White,
            Black,
            Green,
            Blue,
            Yellow,
            Gray,
            Cyan,
            Magenta,
            Orange,
            Pink,
            Brown,
            Violet,
            Purple,
            Lime
        }

        public static Dictionary<HtmlColor, string> Colors = new Dictionary<HtmlColor, string>
        {
            { HtmlColor.Red,     "red" },
            { HtmlColor.White,   "white" },
            { HtmlColor.Black,   "black" },
            { HtmlColor.Green,   "green" },
            { HtmlColor.Blue,    "blue" },
            { HtmlColor.Yellow,  "yellow" },
            { HtmlColor.Gray,    "gray" },
            { HtmlColor.Cyan,    "cyan" },
            { HtmlColor.Magenta, "magenta" },
            { HtmlColor.Orange,  "orange" },
            { HtmlColor.Pink,    "pink" },
            { HtmlColor.Brown,   "brown" },
            { HtmlColor.Violet,  "violet" },
            { HtmlColor.Purple,  "purple" },
            { HtmlColor.Lime,    "lime" },
        };
    }
}