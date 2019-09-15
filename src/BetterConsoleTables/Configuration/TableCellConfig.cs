using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Configuration
{
    public class TableCellConfig
    {
        /// <summary>
        /// Creates a TableCellConfig with the default Foreground Color of LightGrey
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="plane"></param>
        public TableCellConfig(Alignment alignment = Alignment.Left, ColorPlane plane = ColorPlane.Foreground)
        {
            ForegoundColor = Constants.DefaultForegoundColor;
            BackgroundColor = Constants.DefaultBackgroundColor;
            Alignment = alignment;
            Plane = plane;
        }

        public TableCellConfig(Color foregroundColor, Alignment alignment = Constants.DefaultAlignment, ColorPlane plane = ColorPlane.Foreground)
            :this(alignment, plane)
        {
            ForegoundColor = foregroundColor;
        }

        public TableCellConfig(Color foregroundColor, Color backgroundColor, Alignment alignment = Constants.DefaultAlignment, ColorPlane plane = ColorPlane.Foreground)
            : this(foregroundColor, alignment, plane)
        {
            BackgroundColor = backgroundColor;
        }

        public Color ForegoundColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Alignment Alignment { get; set; }
        public ColorPlane Plane { get; set; }
    }

    /*public class TableCellConfigBuilder
    {
        public TableCellConfigBuilder With(Action<TableCellConfig> action)
        {
            With(x => x.Alignment = Alignment.Center);
        }
    }*/
}
