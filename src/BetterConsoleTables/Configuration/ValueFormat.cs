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

        /// <summary>
        /// Creates a TableCellConfig with the default Foreground Color of LightGrey
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="plane"></param>
        public ValueFormat(Alignment alignment = Alignment.Left)
        {
            ForegoundColor = Constants.DefaultForegoundColor;
            BackgroundColor = Constants.DefaultBackgroundColor;
            Alignment = alignment;
        }

        public ValueFormat(Color foregroundColor, Alignment alignment = Constants.DefaultAlignment)
            :this(alignment)
        {
            ForegoundColor = foregroundColor;
        }

        public ValueFormat(Color foregroundColor, Color backgroundColor, Alignment alignment = Constants.DefaultAlignment)
            : this(foregroundColor, alignment)
        {
            BackgroundColor = backgroundColor;
        }

        public Color ForegoundColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Alignment Alignment { get; set; }
    }

    /*public class TableCellConfigBuilder
    {
        public TableCellConfigBuilder With(Action<TableCellConfig> action)
        {
            With(x => x.Alignment = Alignment.Center);
        }
    }*/
}
