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
        public static string AddFormatting(this string value, FormatType formats)
        {
            throw new NotImplementedException();
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
