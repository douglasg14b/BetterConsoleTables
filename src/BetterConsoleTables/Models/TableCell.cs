using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    public class TableCell<TValue>
    {
        public TableCell(TValue value, TableCellConfig config)
        {
            Value = value.ToString();
            Color = config.Color;
            Alignment = config.Alignment;
        }

        public TableCell(TValue value) :this(value, new TableCellConfig()) { }

        public string Value { get; set; }
        public ConsoleColor Color { get; set; }
        public Alignment Alignment { get; set; }

        public int Rows { get; } //Will fill out later
    }

    public class TableCell : TableCell<string>
    {
        public TableCell(string value, TableCellConfig config)
            :base(value, config) {}

        public TableCell(string value)
            : base(value, new TableCellConfig()) { }
    }
}
