using BetterConsoleTables.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    public class ValueFormat
    {
        public ValueFormat() { }

        public ValueFormat(Alignment alignment = Constants.DefaultAlignment, Color foregroundColor = default, Color backgroundColor = default)
        {
            Alignment = alignment;
            ForegoundColor = foregroundColor == default ? Constants.DefaultForegoundColor : foregroundColor;
            BackgroundColor = backgroundColor == default ? Constants.DefaultForegoundColor : backgroundColor;
        }

        public Color ForegoundColor { get; set; } = Constants.DefaultForegoundColor;
        public Color BackgroundColor { get; set; } = Constants.DefaultBackgroundColor;
        public Alignment Alignment { get; set; } = Constants.DefaultAlignment;

        public ValueFormat WithForegoundColor(Color color)
        {
            ForegoundColor = color;
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
