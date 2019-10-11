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
        public static readonly string AnsiReset = AnsiEsc + "[0m";
        public static readonly string AnsiNoUnderline = AnsiEsc + "[24m";
        public static readonly string AnsiBoldOff = AnsiEsc + "[22m";
        public static readonly string AnsiNormalColor = AnsiEsc + "[22m";
        public static readonly string AnsiBlinkStop = AnsiEsc + "[25m";
        public static readonly string AnsiNotCrossedOut = AnsiEsc + "[29m";

        public static readonly string AnsiBold = AnsiEsc + "[1m{0}" + AnsiReset;      
        public static readonly string AnsiUnderline = AnsiEsc + "[4m{0}" + AnsiNoUnderline;
        public static readonly string AnsiReversedColors = AnsiEsc + "[7m{0}" + AnsiReset;
        public static readonly string AnsiCrossedOut = AnsiEsc + "[9m{0}" + AnsiReset; // Not W


        public static readonly string AnsiBlink = AnsiEsc + "[5m{0}" + AnsiReset; // Not on Windows Console. Not When unfocused
        public static readonly string AnsiOverline = AnsiEsc + "[53m{0}" + AnsiReset; // Not W
        public static readonly string AnsiItalic = AnsiEsc + "[3m{0}" + AnsiReset; // Not W




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
        public static readonly string FullColorFormat = ColorFormat + "{4}" + AnsiReset;

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
        public static readonly string SimpleFullColorFormat = SimpleColorFormat + "{2}" + AnsiReset;
        

    }
}
