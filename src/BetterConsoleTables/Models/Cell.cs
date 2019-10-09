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
        public Cell(TValue value, ValueFormat config)
        {
            Value = value.ToString();
            Config = config;
        }

        public Cell(TValue value) :this(value, new ValueFormat()) { }


        public string Value { get; set; }
        public ValueFormat Config { get; set; }
    }

    public class TableCell : Cell<string>
    {
        public TableCell(string value, ValueFormat config)
            :base(value, config) {}

        public TableCell(string value)
            : base(value, new ValueFormat()) { }
    }
}
