using static Core.Utility.DebugTool.DebugColorOptions;

namespace Core.Utility.DebugTool
{
    public static class DebugLoggerFormate
    {
        public static string Color(this string myStr, string color)
        {
            return $"<color={color}>{myStr}</color>";
        }

        public static string Color(this object myStr, HtmlColor color)
        {
            if (myStr is null)
                return "";

            try
            {
                myStr.ToString();
            }
            catch
            {
                throw new System.Exception("Cannot convert object to string!");
            }

            if (color is HtmlColor.Bool)
            {
                if (myStr is bool result)
                {
                    color = result ? HtmlColor.Green : HtmlColor.Red;
                }
                else color = HtmlColor.Gray;
            }

            string resultColor = Colors[color];
            return $"<color={resultColor}>{myStr}</color>";
        }

        public static string Size(this string myStr, int fontSize)
        {
            return $"<size={fontSize}>{myStr}</size>";
        }
    }
}