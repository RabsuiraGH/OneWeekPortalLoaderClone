namespace Core.Utility.DebugTool
{
    public static class DebugLoggerFormate
    {
        public static string Color(this string myStr, string color)
        {
            return $"<color={color}>{myStr}</color>";
        }

        public static string Size(this string myStr, int fontSize)
        {
            return $"<size={fontSize}>{myStr}</size>";
        }
    }
}