using BetterConsoleTables.Models;
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

namespace BetterConsoleTables
{
    public static class Extensions
    {

        internal static FormatType Merge(this FontStyle style, ColorFormatType colorFormat)
        {
            return (FormatType)((int)style | (int)colorFormat);
        }

        public static uint BitCount(this FontStyle styles)
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

        public static string[] GetFontStyleCodes(this FontStyle styles, int extraSize = 0) // Extra size is for this being used in a format with a value
        {
            uint styleCount = styles.BitCount();
            string[] styleCodes = new string[styleCount + extraSize];

            // Unrolled loop, for questionable performance gains...
            int i = 0;
            if(FontStyle.Blink == (styles & FontStyle.Blink))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyle.Blink];

            if (FontStyle.Bold == (styles & FontStyle.Bold))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyle.Bold];

            if (FontStyle.CrossedOut == (styles & FontStyle.CrossedOut))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyle.CrossedOut];

            if (FontStyle.Italic == (styles & FontStyle.Italic))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyle.Italic];

            if (FontStyle.Overline == (styles & FontStyle.Overline))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyle.Overline];

            if (FontStyle.Underline == (styles & FontStyle.Underline))
                styleCodes[i++] = Ansi.FontStyleLookup[FontStyle.Underline];


            return styleCodes;
        }


        public static string GetFormatCodes(this FontStyle style)
        {
            FormatType formats = FormatType.BackgroundColor;
            FormatType combined = (FormatType)((int)formats | (int)style);
            return combined.ToString();
        }

        // Extra size is if the caller needs extra array items to put strings into for String.Format
        public static string[] GetAnsiCodes(this ValueFormat format, int extraArraySize = 0) 
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
            if (FontStyle.Blink == (styles & FontStyle.Blink))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyle.Blink];

            if (FontStyle.Bold == (styles & FontStyle.Bold))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyle.Bold];

            if (FontStyle.CrossedOut == (styles & FontStyle.CrossedOut))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyle.CrossedOut];

            if (FontStyle.Italic == (styles & FontStyle.Italic))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyle.Italic];

            if (FontStyle.Overline == (styles & FontStyle.Overline))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyle.Overline];

            if (FontStyle.Underline == (styles & FontStyle.Underline))
                ansiCodes[i++] = Ansi.FontStyleLookup[FontStyle.Underline];

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


        /// <summary>
        /// Adds non-color, flag-based, formatting to a string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static string SetStyle(this string value, FontStyle formats)
        {
            string[] styleCodes = formats.GetFontStyleCodes(1); // Add 1 to end of styles for value
            styleCodes[styleCodes.Length - 1] = value;

            return String.Format(Ansi.FormatStrings[styleCodes.Length - 1], styleCodes);
        }

        public static string SetStyle(this string value, ValueFormat format)
        {
            string[] ansiCodes = format.GetAnsiCodes(1);
            ansiCodes[ansiCodes.Length - 1] = value;

            return String.Format(Ansi.FormatStrings[ansiCodes.Length - 1], ansiCodes);
        }



        public static string Bold(this string value)
        {
            return string.Format(Constants.AnsiBold, value);
        }

        public static string Underline(this string value)
        {
            return string.Format(Constants.AnsiUnderline, value);
        }

        public static string CrossedOut(this string value)
        {
            return string.Format(Constants.AnsiCrossedOut, value);
        }

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

        public static string WithForegroundGradient(this string value, Color start, Color end)
        {
            int whiteSpaceCount = 0;
            for(int i = 0; i < value.Length; i++)
            {
                if (Char.IsWhiteSpace(value[i]))
                {
                    whiteSpaceCount++;
                }
            }

            List<Color> colors = GetGradients(start, end, value.Length - whiteSpaceCount).ToList();
            string[] outputs = new string[value.Length];

            int colorIndex = 0;
            for(int i = 0; i < value.Length; i++)
            {   
                // Don't use gradients on whitespace
                if (Char.IsWhiteSpace(value[i]))
                {
                    outputs[i] = value[i].ToString();
                    continue;
                }

                outputs[i] = string.Format(Constants.FullColorFormat, 
                    (int)ColorPlane.Foreground, 
                    colors[colorIndex].R, 
                    colors[colorIndex].G, 
                    colors[colorIndex].B, 
                    value[i]);
                colorIndex++;
            }
            return String.Join("", outputs);
        }

        public static IEnumerable<Color> GetGradients(Color start, Color end, int steps)
        {
            int stepA = ((end.A - start.A) / (steps - 1));
            int stepR = ((end.R - start.R) / (steps - 1));
            int stepG = ((end.G - start.G) / (steps - 1));
            int stepB = ((end.B - start.B) / (steps - 1));

            for (int i = 0; i < steps; i++)
            {
                yield return Color.FromArgb(start.A + (stepA * i),
                                            start.R + (stepR * i),
                                            start.G + (stepG * i),
                                            start.B + (stepB * i));
            }
        }
    }
}
