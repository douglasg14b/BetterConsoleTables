using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Core
{
    public enum ColorPlane
    {
        Foreground = 38,
        Background = 48
    }

    /// <summary>
    /// Extended Font Styles
    /// </summary>
    [Flags]
    public enum FontStyleExt
    {
        None = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Blink = 8,
        CrossedOut = 16,
        Overline = 32
    }

    [Flags]
    public enum FormatType
    {
        None = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Blink = 8,
        CrossedOut = 16,
        Overline = 32,
        ForegroundColor = 64,
        BackgroundColor = 128
    }

    [Flags]
    public enum ColorFormatType
    {
        None = 0,
        Foreground = 64,
        Background = 128
    }
}
