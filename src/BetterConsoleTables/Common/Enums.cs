using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public enum Alignment
    {
        Left = 0,
        Right = 1,
        Center = 2
    }

    public enum ColorPlane
    {
        Foreground = 38,
        Background = 48
    }

    [Flags]
    public enum FormatType
    {
        None = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Overline = 8,
        CrossedOut = 16,
        Blink = 32
    }

    [Flags]
    internal enum AllFormatType
    {
        None = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Overline = 8,
        CrossedOut = 16,
        Blink = 32,
        ForegroundColor = 64,
        BackgroundColor = 128
    }

    [Flags]
    internal enum ColorFormatType
    {
        None = 0,
        Foreground = 64,
        Background = 128
    }


}
