using BetterConsoles.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Colors
{

    /// <summary>
    /// Formatting for a console value
    /// Controls both foreground & background color, and font styles
    /// </summary>
    public class Format : IFormat
    {
        public Format() { }

        public Format(Color foregroundColor = default,
            Color backgroundColor = default,
            FontStyleExt fontStyle = FontStyleExt.None)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            FontStyle = fontStyle;
        }

        public Color ForegroundColor { get; set; }
        public Color BackgroundColor { get; set; }
        public FontStyleExt FontStyle { get; set; }


        public bool DefaultColors => DefaultForeground && DefaultBackground;
        public bool DefaultForeground => ForegroundColor == default;
        public bool DefaultBackground => BackgroundColor == default;

        public static Format Default()
        {
            return new Format();
        }
    }
}
