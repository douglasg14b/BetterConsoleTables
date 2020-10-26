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
        public IColumn column;

        public ColumnBuilder(string columnTitle)
            : this(new Column(columnTitle)) { }

        public ColumnBuilder(object columnTitle)
            : this(new Column(columnTitle)) { }

        public ColumnBuilder(IColumn column)
        {
            this.column = column;
        }

        public IColumn GetColumn()
        {
            return column;
        }

        public IColumnValueFormatBuilder<IColumnBuilder> WithRowsFormat() => WithRowsFormat(new CellFormat());
        public IColumnValueFormatBuilder<IColumnBuilder> WithRowsFormat(CellFormat format)
        {
            column.RowsFormat = format;
            return new ColumnValueFormatBuilder(format, this);
        }

        public IColumnBuilder WithRowsAlignment(Alignment alignment)
        {
            if(column.RowsFormat is null)
            {
                column.RowsFormat = new CellFormat();
            }
            column.RowsFormat.Alignment = alignment;

            return this;
        }


        public IColumnValueFormatBuilder<IColumnBuilder> WithHeaderFormat() => WithHeaderFormat(new CellFormat());
        public IColumnValueFormatBuilder<IColumnBuilder> WithHeaderFormat(CellFormat format)
        {
            column.HeaderFormat = format;
            return new ColumnValueFormatBuilder(format, this);
        }

        public IColumnBuilder WithHeaderAlignment(Alignment alignment)
        {
            if (column.HeaderFormat is null)
            {
                column.HeaderFormat = new CellFormat();
            }
            column.HeaderFormat.Alignment = alignment;

            return this;
        }
    }
}
