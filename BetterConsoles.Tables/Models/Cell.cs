using BetterConsoles.Tables.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Models
{
    public class Cell<TValue> : ICell
    {
        public Cell(TValue value)
            : this(value, new CellFormat()) { }

        public Cell(TValue value, CellFormat format)
        {
            Value = value.ToString();
            Format = format;
        }

        public string Value { get; set; }
        public ICellFormat Format { get; set; }
    }

    public class TableCell : Cell<string>
    {
        public TableCell(string value, CellFormat config)
            :base(value, config) {}

        public TableCell(string value)
            : base(value, new CellFormat()) { }
    }
}
