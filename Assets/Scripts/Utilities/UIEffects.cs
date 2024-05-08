
using UnityEngine;


namespace Utilities
{
    public static class UIEffects
    {
        public static Color HexToColor(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out Color color);
            return color;
        }
    }
}