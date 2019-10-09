using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders
{
    public class ColumnBuilder : IColumnBuilder
    {
        public FormattedColumn column;

        public ColumnBuilder(string columnTitle)
            : this(new FormattedColumn(columnTitle)) { }

        public ColumnBuilder(FormattedColumn column)
        {
            this.column = column;
        }

        public FormattedColumn GetColumn()
        {
            return column;
        }

        public IColumnValueFormatBuilder WithRowsFormat() => WithRowsFormat(new ValueFormat());
        public IColumnValueFormatBuilder WithRowsFormat(ValueFormat format)
        {
            column.RowsFormat = format;
            return new ColumnValueFormatBuilder(format, this);
        }

        public IColumnValueFormatBuilder WithHeaderFormat() => WithHeaderFormat(new ValueFormat());
        public IColumnValueFormatBuilder WithHeaderFormat(ValueFormat format)
        {
            column.HeaderFormat = format;
            return new ColumnValueFormatBuilder(format, this);
        }
    }
}
