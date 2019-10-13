using BetterConsole.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
#if NETCOREAPP3_0
using System.Runtime.Intrinsics.X86;
#endif
using System.Text;
using System.Threading.Tasks;

namespace BetterConsole.Colors.Extensions
{
    public static class FormatExtensions
    {

        internal static FormatType Merge(this FontStyleExt style, ColorFormatType colorFormat)
        {
            return (FormatType)((int)style | (int)colorFormat);
        }

        public static uint BitCount(this FontStyleExt styles)
        {
#if NETCOREAPP3_0
            return Popcnt.PopCount((uint)styles);
#else
            uint count = 0;
            while (styles != 0)
            {
                count++;
                styles &= (styles - 1);
            }
            return count;
#endif
        }

        // Extra size is for this being used in a format with a value
        public static string[] GetAnsiCodes(this FontStyleExt styles, int extraSize = 0)
        {
            uint styleCount = styles.BitCount();
            string[] styleCodes = new string[styleCount + extraSize];

            // Unrolled loop, for questionable performance gains...
            int i = 0;
            if(FontStyleExt.Blink == (styles & FontStyleExt.Blink))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Blink];

            if (FontStyleExt.Bold == (styles & FontStyleExt.Bold))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Bold];

            if (FontStyleExt.CrossedOut == (styles & FontStyleExt.CrossedOut))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.CrossedOut];

            if (FontStyleExt.Italic == (styles & FontStyleExt.Italic))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Italic];

            if (FontStyleExt.Overline == (styles & FontStyleExt.Overline))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Overline];

            if (FontStyleExt.Underline == (styles & FontStyleExt.Underline))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Underline];


            return styleCodes;
        }

        // Extra size is if the caller needs extra array items to put strings into for String.Format
        public static string[] GetAnsiCodes(this Format format, int extraArraySize = 0) 
        {
            var styles = format.FontStyle;
            int colorCodeCount = 0;
            int styleCount = (int)format.FontStyle.BitCount();

            if (!format.DefaultForeground)
                colorCodeCount += 5; // # of codes that go into setting RGB color
            if (!format.DefaultBackground)
                colorCodeCount += 5;

            string[] ansiCodes = new string[styleCount + colorCodeCount + extraArraySize];

            // Unrolled loop, for questionable performance gains...
            int i = 0;
            if (FontStyleExt.Blink == (styles & FontStyleExt.Blink))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Blink];

            if (FontStyleExt.Bold == (styles & FontStyleExt.Bold))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Bold];

            if (FontStyleExt.CrossedOut == (styles & FontStyleExt.CrossedOut))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.CrossedOut];

            if (FontStyleExt.Italic == (styles & FontStyleExt.Italic))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Italic];

            if (FontStyleExt.Overline == (styles & FontStyleExt.Overline))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Overline];

            if (FontStyleExt.Underline == (styles & FontStyleExt.Underline))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyleExt.Underline];

            if (!format.DefaultForeground)
            {
                ansiCodes[i++] = Ansi.Codes.Foreground;
                ansiCodes[i++] = Ansi.Codes.ColorRgb;
                ansiCodes[i++] = format.ForegroundColor.R.ToString();
                ansiCodes[i++] = format.ForegroundColor.G.ToString();
                ansiCodes[i++] = format.ForegroundColor.B.ToString();
            }

            if (!format.DefaultBackground)
            {
                ansiCodes[i++] = Ansi.Codes.Background;
                ansiCodes[i++] = Ansi.Codes.ColorRgb;
                ansiCodes[i++] = format.BackgroundColor.R.ToString();
                ansiCodes[i++] = format.BackgroundColor.G.ToString();
                ansiCodes[i++] = format.BackgroundColor.B.ToString();
            }

            return ansiCodes;
        }
    }
}
