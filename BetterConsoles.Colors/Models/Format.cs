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
            ForegroundColor = foregroundColor == default ? Constants.DefaultForegroundColor : foregroundColor;
            BackgroundColor = backgroundColor == default ? Constants.DefaultForegroundColor : backgroundColor;
            FontStyle = fontStyle;
        }

        public Color ForegroundColor { get; set; } = Constants.DefaultForegroundColor;
        public Color BackgroundColor { get; set; } = Constants.DefaultBackgroundColor;
        public FontStyleExt FontStyle { get; set; } = FontStyleExt.None;


        public bool DefaultColors => DefaultForeground && DefaultBackground;
        public bool DefaultForeground => ForegroundColor == Constants.DefaultForegroundColor;
        public bool DefaultBackground => BackgroundColor == Constants.DefaultBackgroundColor;

        public static Format Default()
        {
            return new Format();
        }
    }
}
