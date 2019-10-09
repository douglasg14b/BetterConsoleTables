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
        public Column column;

        public ColumnBuilder(string columnTitle)
            : this(new Column(columnTitle)) { }

        public ColumnBuilder(object columnTitle)
            : this(new Column(columnTitle)) { }

        public ColumnBuilder(Column column)
        {
            this.column = column;
        }

        public Column GetColumn()
        {
            return column;
        }

        public IColumnValueFormatBuilder WithRowsFormat() => WithRowsFormat(new ValueFormat());
        public IColumnValueFormatBuilder WithRowsFormat(ValueFormat format)
        {
            column.RowsFormat = format;
            return new ColumnValueFormatBuilder(format, this);
        }

        public IColumnBuilder WithRowsAlignment(Alignment alignment)
        {
            if(column.RowsFormat is null)
            {
                column.RowsFormat = new ValueFormat();
            }
            column.RowsFormat.Alignment = alignment;

            return this;
        }


        public IColumnValueFormatBuilder WithHeaderFormat() => WithHeaderFormat(new ValueFormat());
        public IColumnValueFormatBuilder WithHeaderFormat(ValueFormat format)
        {
            column.HeaderFormat = format;
            return new ColumnValueFormatBuilder(format, this);
        }

        public IColumnBuilder WithHeaderAlignment(Alignment alignment)
        {
            if (column.HeaderFormat is null)
            {
                column.HeaderFormat = new ValueFormat();
            }
            column.HeaderFormat.Alignment = alignment;

            return this;
        }
    }
}
