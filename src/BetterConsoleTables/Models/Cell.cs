using BetterConsoleTables.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    public class Cell<TValue>
    {
        public Cell(TValue value, TableCellConfig config)
        {
            Value = value.ToString();
            Config = config;
        }

        public Cell(TValue value) :this(value, new TableCellConfig()) { }


        public string Value { get; set; }
        public TableCellConfig Config { get; set; }
    }

    public class TableCell : Cell<string>
    {
        public TableCell(string value, TableCellConfig config)
            :base(value, config) {}

        public TableCell(string value)
            : base(value, new TableCellConfig()) { }
    }
}
