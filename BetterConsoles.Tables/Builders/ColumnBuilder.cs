using BetterConsoles.Tables.Builders.Interfaces;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders
{

    public class ColumnBuilder : IStandaloneColumnBuilder
    {
        private IColumn column;

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

        public IStandaloneColumnValueFormatBuilder WithRowsFormat() => WithRowsFormat(new CellFormat());
        public IStandaloneColumnValueFormatBuilder WithRowsFormat(CellFormat format)
        {
            column.RowsFormat = format;
            return new StandaloneColumnValueFormatBuilder(format, this);
        }

        public IStandaloneColumnBuilder WithRowsAlignment(Alignment alignment)
        {
            if(column.RowsFormat is null)
            {
                column.RowsFormat = new CellFormat();
            }
            column.RowsFormat.Alignment = alignment;

            return this;
        }


        public IStandaloneColumnValueFormatBuilder WithHeaderFormat() => WithHeaderFormat(new CellFormat());
        public IStandaloneColumnValueFormatBuilder WithHeaderFormat(CellFormat format)
        {
            column.HeaderFormat = format;
            return new StandaloneColumnValueFormatBuilder(format, this);
        }

        public IStandaloneColumnBuilder WithHeaderAlignment(Alignment alignment)
        {
            if (column.HeaderFormat is null)
            {
                column.HeaderFormat = new CellFormat();
            }
            column.HeaderFormat.Alignment = alignment;

            return this;
        }
    }

    internal class TableColumnBuilder : ITableColumnBuilder
    {
        private ITableBuilder instance;
        private IColumn column;

        public TableColumnBuilder(string columnTitle, ITableBuilder instance)
            : this(new Column(columnTitle), instance) { }

        public TableColumnBuilder(IColumn column, ITableBuilder instance)
        {
            this.column = column;
            this.instance = instance;
        }

        internal IColumn GetColumn() => column;

        public ITableColumnValueFormatBuilder WithRowsFormat() => WithRowsFormat(new CellFormat());
        public ITableColumnValueFormatBuilder WithRowsFormat(CellFormat format)
        {
            column.RowsFormat = format;
            return new TableColumnValueFormatBuilder(format, this);
        }

        public ITableColumnBuilder WithRowsAlignment(Alignment alignment)
        {
            if (column.RowsFormat is null)
            {
                column.RowsFormat = new CellFormat();
            }
            column.RowsFormat.Alignment = alignment;

            return this;
        }


        public ITableColumnValueFormatBuilder WithHeaderFormat() => WithHeaderFormat(new CellFormat());
        public ITableColumnValueFormatBuilder WithHeaderFormat(CellFormat format)
        {
            column.HeaderFormat = format;
            return new TableColumnValueFormatBuilder(format, this);
        }

        public ITableColumnBuilder WithHeaderAlignment(Alignment alignment)
        {
            if (column.HeaderFormat is null)
            {
                column.HeaderFormat = new CellFormat();
            }
            column.HeaderFormat.Alignment = alignment;

            return this;
        }

        public ITableColumnBuilder WithColumn(string columnTitle) => instance.WithColumn(columnTitle);

        public ITableColumnBuilder WithColumn(IColumn column) => instance.WithColumn(column);

        public Table Build() => instance.Build();
    }
}
