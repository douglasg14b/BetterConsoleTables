using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public static class Extensions
    {
        public static string WithColor(this string value, Color color, ColorPlane plane)
        {
            string output = string.Format(Constants.FullColorFormat, (int)plane, color.R, color.G, color.B, value);
            return output;
        }

        public static string WithForegroundColor(this string value, Color color)
        {
            return value.WithColor(color, ColorPlane.Foreground);
        }

        public static string WithBackgroundColor(this string value, Color color)
        {
            return value.WithColor(color, ColorPlane.Background);
        }
    }
}
