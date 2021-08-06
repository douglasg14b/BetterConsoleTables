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

        public IStandaloneColumnValueFormatBuilder RowsFormat() => RowsFormat(new CellFormat());
        public IStandaloneColumnValueFormatBuilder RowsFormat(CellFormat format)
        {
            column.RowsFormat = format;
            return new StandaloneColumnValueFormatBuilder(format, this);
        }

        public IStandaloneColumnBuilder RowsAlignment(Alignment alignment)
        {
            if(column.RowsFormat is null)
            {
                column.RowsFormat = new CellFormat();
            }
            column.RowsFormat.Alignment = alignment;

            return this;
        }


        public IStandaloneColumnValueFormatBuilder HeaderFormat() => HeaderFormat(new CellFormat());
        public IStandaloneColumnValueFormatBuilder HeaderFormat(CellFormat format)
        {
            column.HeaderFormat = format;
            return new StandaloneColumnValueFormatBuilder(format, this);
        }

        public IStandaloneColumnBuilder HeaderAlignment(Alignment alignment)
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

        public ITableColumnValueFormatBuilder RowsFormat() => RowsFormat(new CellFormat());
        public ITableColumnValueFormatBuilder RowsFormat(CellFormat format)
        {
            column.RowsFormat = format;
            return new TableColumnValueFormatBuilder(format, this);
        }

        public ITableColumnBuilder RowsAlignment(Alignment alignment)
        {
            if (column.RowsFormat is null)
            {
                column.RowsFormat = new CellFormat();
            }
            column.RowsFormat.Alignment = alignment;

            return this;
        }


        public ITableColumnValueFormatBuilder HeaderFormat() => HeaderFormat(new CellFormat());
        public ITableColumnValueFormatBuilder HeaderFormat(CellFormat format)
        {
            column.HeaderFormat = format;
            return new TableColumnValueFormatBuilder(format, this);
        }

        public ITableColumnBuilder HeaderAlignment(Alignment alignment)
        {
            if (column.HeaderFormat is null)
            {
                column.HeaderFormat = new CellFormat();
            }
            column.HeaderFormat.Alignment = alignment;

            return this;
        }

        public ITableColumnBuilder AddColumn(string columnTitle, ICellFormat headerFormat = null, ICellFormat rowsFormat = null) => instance.AddColumn(columnTitle, headerFormat, rowsFormat);

        public ITableColumnBuilder AddColumn(IColumn column) => instance.AddColumn(column);

        public Table Build() => instance.Build();
    }
}
