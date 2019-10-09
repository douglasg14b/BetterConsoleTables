using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Configuration
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
    }

    /*public class ValueFormatBuilder
    {
        public TableCellConfigBuilder With(Action<ValueFormat> action)
        {
            With(x => x.Alignment = Alignment.Center);
        }
    }*/
}
