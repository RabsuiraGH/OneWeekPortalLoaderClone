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
            { HtmlColor.Red,     "Red" },
            { HtmlColor.White,   "White" },
            { HtmlColor.Black,   "Black" },
            { HtmlColor.Green,   "Green" },
            { HtmlColor.Blue,    "Blue" },
            { HtmlColor.Yellow,  "Yellow" },
            { HtmlColor.Gray,    "Gray" },
            { HtmlColor.Cyan,    "Cyan" },
            { HtmlColor.Magenta, "Magenta" },
            { HtmlColor.Orange,  "Orange" },
            { HtmlColor.Pink,    "Pink" },
            { HtmlColor.Brown,   "Brown" },
            { HtmlColor.Violet,  "Violet" },
            { HtmlColor.Purple,  "Purple" },
            { HtmlColor.Lime,    "Lime" },
        };
    }
}