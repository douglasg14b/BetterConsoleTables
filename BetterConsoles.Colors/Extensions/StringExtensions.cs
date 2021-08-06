using BetterConsoles.Colors.Common;
using BetterConsoles.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BetterConsoles.Colors.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Adds non-color, flag-based, formatting to a string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static string SetStyle(this string value, FontStyleExt formats)
        {
            string[] ansiCodes = formats.GetAnsiCodes(1); // Add 1 to end of styles for value
            if (ansiCodes.Length == 1)
            {
                return value;
            }

            ansiCodes[ansiCodes.Length - 1] = value;

            return String.Format(Ansi.FormatStrings[ansiCodes.Length - 1], ansiCodes);
        }

        /// <summary>
        /// Adds all the formatting in <param name="format"/> to the provided string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string SetStyle(this string value, IFormat format)
        {
            string[] ansiCodes = format.GetAnsiCodes(1);
            if (ansiCodes.Length == 1)
            {
                return value;
            }

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

        public static string Color(this string value, Color color, ColorPlane plane)
        {
            string output = string.Format(Constants.FullColorFormat, (int)plane, color.R, color.G, color.B, value);
            return output;
        }

        public static string ForegroundColor(this string value, Color color)
        {
            return value.Color(color, ColorPlane.Foreground);
        }

        public static string WithBackgroundColor(this string value, Color color)
        {
            return value.Color(color, ColorPlane.Background);
        }

        public static string ForegroundGradient(this string value, Color start, Color end)
        {
            int whiteSpaceCount = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsWhiteSpace(value[i]))
                {
                    whiteSpaceCount++;
                }
            }

            List<Color> colors = Helpers.GetGradients(start, end, value.Length - whiteSpaceCount).ToList();
            string[] outputs = new string[value.Length];

            int colorIndex = 0;
            for (int i = 0; i < value.Length; i++)
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
    }
}
