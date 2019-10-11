using BetterConsoleTables.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    /// <summary>
    /// The Format configuration that will be applied to a table cell or column
    /// </summary>
    public class ValueFormat
    {
        public ValueFormat() { }

        public ValueFormat(Alignment alignment = Constants.DefaultAlignment, 
            Color foregroundColor = default, 
            Color backgroundColor = default,
            FormatType formats = FormatType.None)
        {
            Alignment = alignment;
            ForegroundColor = foregroundColor == default ? Constants.DefaultForegroundColor : foregroundColor;
            BackgroundColor = backgroundColor == default ? Constants.DefaultForegroundColor : backgroundColor;
            Formats = formats;
        }

        public Color ForegroundColor { get; set; } = Constants.DefaultForegroundColor;
        public Color BackgroundColor { get; set; } = Constants.DefaultBackgroundColor;
        public Alignment Alignment { get; set; } = Constants.DefaultAlignment;
        public FormatType Formats { get; set; } = FormatType.None;


        public bool DefaultColors => DefaultForeground && DefaultBackground;
        public bool DefaultForeground => ForegroundColor == Constants.DefaultBackgroundColor;
        public bool DefaultBackground => BackgroundColor == Constants.DefaultBackgroundColor;

        public static ValueFormat Default()
        {
            return new ValueFormat();
        }
    }


}
