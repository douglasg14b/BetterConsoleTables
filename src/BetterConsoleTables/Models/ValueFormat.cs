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

        public ValueFormat(Alignment alignment = Constants.DefaultAlignment, Color foregroundColor = default, Color backgroundColor = default)
        {
            Alignment = alignment;
            ForegroundColor = foregroundColor == default ? Constants.DefaultForegroundColor : foregroundColor;
            BackgroundColor = backgroundColor == default ? Constants.DefaultForegroundColor : backgroundColor;
        }

        public Color ForegroundColor { get; set; } = Constants.DefaultForegroundColor;
        public Color BackgroundColor { get; set; } = Constants.DefaultBackgroundColor;
        public Alignment Alignment { get; set; } = Constants.DefaultAlignment;

        public ValueFormat WithForegroundColor(Color color)
        {
            ForegroundColor = color;
            return this;
        }
        public ValueFormat WithBackgroundColor(Color color)
        {
            BackgroundColor = color;
            return this;
        }
        public ValueFormat WithAlignment(Alignment alignment)
        {
            Alignment = alignment;
            return this;
        }

        public static ValueFormat Default()
        {
            return new ValueFormat();
        }
    }


}
