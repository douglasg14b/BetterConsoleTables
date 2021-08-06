using BetterConsoles.Tables.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Models
{
    public class Cell : Cell<string>
    {
        public Cell(string value)
            : this(value, new CellFormat()) { }

        public Cell(string value, CellFormat format)
            :base(value, format) { }
    }

    public class Cell<TValue> : ICell
    {
        public Cell(TValue value)
            : this(value, new CellFormat()) { }

        public Cell(TValue value, CellFormat format)
        {
            Value = value.ToString();
            Format = format;
        }

        public Cell(TValue value, CellFormat format, Func<TValue, string> formatCallback)
        {
            Value = formatCallback(value);
            FormatCallback = formatCallback;
            Format = format;
        }

        public Cell(TValue value, Func<TValue, string> formatCallback)
        {
            Value = formatCallback(value);
            FormatCallback = formatCallback;
            Format = new CellFormat() { InnerFormatting = true };
        }

        public string Value { get; set; }
        public ICellFormat Format { get; set; } = new CellFormat();

        public Func<TValue, string> FormatCallback { get; set; }
    }

    public class TableCell : Cell<string>
    {
        public TableCell(string value, CellFormat config)
            :base(value, config) {}

        public TableCell(string value)
            : base(value, new CellFormat()) { }
    }
}
