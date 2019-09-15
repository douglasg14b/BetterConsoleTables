using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public class TableCellConfig
    {
        public TableCellConfig(ConsoleColor color = Constants.DefaultColor, Alignment alignment = Constants.DefaultAlignment)
        {
            Color = color;
            Alignment = alignment;
        }

        public ConsoleColor Color { get; set; }
        public Alignment Alignment { get; set; }
    }
}
