using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Core
{
    public static class Ansi
    {
        public const char EscChar1 = '\u001b';
        public const char EscChar2 = '[';
        public const string Esc = "\u001b[";
        public const string End = "m";
        public const string Block = "\u001b[{0}m";

        public const string Reset = Esc + Codes.Reset + End;

        // Looks up values from font style to ANSI code
        public static readonly Dictionary<FontStyleExt, string> FontStyleLookup = new Dictionary<FontStyleExt, string>()
        {
            [FontStyleExt.Bold] = Codes.Enable.Bold,
            [FontStyleExt.Italic] = Codes.Enable.Italic,
            [FontStyleExt.Underline] = Codes.Enable.Underline,
            [FontStyleExt.Blink] = Codes.Enable.Blink,
            [FontStyleExt.CrossedOut] = Codes.Enable.CrossedOut,
            [FontStyleExt.Overline] = Codes.Enable.Overline
        };

        public static readonly Dictionary<int, string> FormatStrings = new Dictionary<int, string>()
        {
            [1] = Esc + "{0}m{1}" + Reset,
            [2] = Esc + "{0};{1}m{2}" + Reset,
            [3] = Esc + "{0};{1};{2}m{3}" + Reset,
            [4] = Esc + "{0};{1};{2};{3}m{4}" + Reset,
            [5] = Esc + "{0};{1};{2};{3};{4}m{5}" + Reset,
            [6] = Esc + "{0};{1};{2};{3};{4};{5}m{6}" + Reset,
            [7] = Esc + "{0};{1};{2};{3};{4};{5};{6}m{7}" + Reset,
            [8] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7}m{8}" + Reset,
            [9] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8}m{9}" + Reset,
            [10] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}m{10}" + Reset,
            [11] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}m{11}" + Reset,
            [12] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}m{12}" + Reset,
            [13] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12}m{13}" + Reset,
            [14] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13}m{14}" + Reset,
            [15] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14}m{15}" + Reset,
            [16] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15}m{16}" + Reset,
            [17] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}m{17}" + Reset,
            [18] = Esc + "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17}m{18}" + Reset,
        };

        public static class Codes
        {
            public const string Reset = "0";

            public const string ColorRgb = "2";
            public const string Foreground = "38";
            public const string Background = "48";

            public static class Enable
            {
                public const string Bold = "1";
                public const string Color = "2";
                public const string Italic = "3"; // Not Windows
                public const string Underline = "4";
                public const string Blink = "5";
                public const string Reversed = "7";
                public const string CrossedOut = "9"; // Not Windows
                public const string Overline = "53"; // Not Windows
            }

            public static class Disable
            {
                public const string Bold = "21";
                public const string Intensity = "22"; // Disables bold
                public const string Italic = "23"; // Not Windows
                public const string Underline = "24";
                public const string Blink = "25";
                public const string Reversed = "27";
                public const string CrossedOut = "29"; // Not Windows
                public const string Overline = "55"; // Not Windows
            }

        }
    }

    public static class Constants
    {
        public static readonly Color _defaultForegroundColor = Color.White;
        public static readonly Color _defaultBackgroundColor = Color.Black;

        public static Color DefaultForegroundColor 
        { 
            get
            {
                if(UserDefinedDefaultForegroundColor == default)
                {
                    return _defaultForegroundColor;
                }
                return UserDefinedDefaultForegroundColor;
            } 
        }

        public static Color DefaultBackgroundColor
        {
            get
            {
                if (UserDefinedDefaultBackgroundColor == default)
                {
                    return _defaultBackgroundColor;
                }
                return UserDefinedDefaultBackgroundColor;
            }
        }

        /// <summary>
        /// A static foreground color that can be defined by the user
        /// </summary>
        public static Color UserDefinedDefaultForegroundColor = default;

        /// <summary>
        /// A static background color that can be defined by the user
        /// </summary>
        public static Color UserDefinedDefaultBackgroundColor = default;

        public const ColorPlane DefaultColorPlane = ColorPlane.Foreground;

        public const string AnsiEsc = "\u001b";
        public const string AnsiReset = AnsiEsc + "[0m";

        public const string AnsiBoldOff = AnsiEsc + "[21m";
        public const string AnsiNormalIntensity = AnsiEsc + "[22m"; //Turns bold off
        public const string AnsiNotItalic = AnsiEsc + "[23m";
        public const string AnsiNoUnderline = AnsiEsc + "[24m";
        public const string AnsiBlinkStop = AnsiEsc + "[25m";
        public const string AnsiNotReversedColors = AnsiEsc + "[27m";
        public const string AnsiNotCrossedOut = AnsiEsc + "[29m";
        public const string AnsiNotOverlined = AnsiEsc + "[55m";

        public const string AnsiBold = AnsiEsc + "[1m{0}";
        public const string AnsiItalic = AnsiEsc + "[3m{0}"; // Not Windows
        public const string AnsiUnderline = AnsiEsc + "[4m{0}";
        public const string AnsiBlink = AnsiEsc + "[5m{0}"; // Not on Windows Console. Not When unfocused
        public const string AnsiReversedColors = AnsiEsc + "[7m{0}";
        public const string AnsiCrossedOut = AnsiEsc + "[9m{0}"; // Not Windows
        public const string AnsiOverline = AnsiEsc + "[53m{0}"; // Not Windows


        public const string AnsiBold_Single = AnsiEsc + "[1m{0}" + AnsiBoldOff;
        public const string AnsiItalic_Single = AnsiEsc + "[3m{0}" + AnsiNotItalic; // Not Windows
        public const string AnsiUnderline_Single = AnsiEsc + "[4m{0}" + AnsiNoUnderline;
        public const string AnsiBlink_Single = AnsiEsc + "[5m{0}" + AnsiBlinkStop; // Not on Windows Console. Not When unfocused
        public const string AnsiReversedColors_Single = AnsiEsc + "[7m{0}" + AnsiNotReversedColors;
        public const string AnsiCrossedOut_Single = AnsiEsc + "[9m{0}" + AnsiNotCrossedOut; // Not Windows
        public const string AnsiOverline_Single = AnsiEsc + "[53m{0}" + AnsiNotOverlined; // Not Windows




        /// <summary>
        /// {0): Plane
        /// {1}: R
        /// {2}: G
        /// {3}: B
        /// </summary>
        public const string ColorFormat = AnsiEsc + "[{0};2;{1};{2};{3}m";

        /// <summary>
        /// {0): Plane
        /// {1}: R
        /// {2}: G
        /// {3}: B
        /// {4}: String Value
        /// </summary>
        public const string FullColorFormat = ColorFormat + "{4}" + AnsiReset;

        /// <summary>
        /// {0): Plane
        /// {1}: Color Number
        /// </summary>
        public const string SimpleColorFormat = AnsiEsc + "[{0};5;{1}m";

        /// <summary>
        /// {0): Plane
        /// {1}: Color Number
        /// {3}: String Value
        /// </summary>
        public const string SimpleFullColorFormat = SimpleColorFormat + "{2}" + AnsiReset;
        

    }
}
