using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public static class Constants
    {
        public static readonly Color DefaultForegroundColor = Color.LightGray;
        public static readonly Color DefaultBackgroundColor = Color.Black;

        public const Alignment DefaultAlignment = Alignment.Left;
        public const ColorPlane DefaultColorPlane = ColorPlane.Foreground;

        public const string AnsiEsc = "\u001b";
        public static readonly string DefaultColorCode = AnsiEsc + "[0m";

        /// <summary>
        /// {0): Plane
        /// {1}: R
        /// {2}: G
        /// {3}: B
        /// </summary>
        public static readonly string ColorFormat = AnsiEsc + "[{0};2;{1};{2};{3}m";

        /// <summary>
        /// {0): Plane
        /// {1}: R
        /// {2}: G
        /// {3}: B
        /// {4}: String Value
        /// </summary>
        public static readonly string FullColorFormat = ColorFormat + "{4}" + DefaultColorCode;

        /// <summary>
        /// {0): Plane
        /// {1}: Color Number
        /// </summary>
        public static readonly string SimpleColorFormat = AnsiEsc + "[{0};5;{1}m";

        /// <summary>
        /// {0): Plane
        /// {1}: Color Number
        /// {3}: String Value
        /// </summary>
        public static readonly string SimpleFullColorFormat = SimpleColorFormat + "{2}" + DefaultColorCode;
        

    }
}
