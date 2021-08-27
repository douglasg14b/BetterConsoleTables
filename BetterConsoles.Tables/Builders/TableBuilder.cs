using BetterConsoles.Tables.Builders.Interfaces;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders
{
    public class TableBuilder : ITableBuilder
    {
        ICellFormat _defaultHeaderFormat = null;
        Queue<TableColumnBuilder> columns = new Queue<TableColumnBuilder>();
        Table table;

        public TableBuilder()
        {
            table = new Table();
        }

        /// <summary>
        /// </summary>
        /// <param name="headersFormat">Will set the defualt header format for all rows</param>
        public TableBuilder(ICellFormat headersFormat)
        {
            table = new Table();
            _defaultHeaderFormat = headersFormat;
        }

        public TableBuilder(TableConfig config)
        {
            table = new Table(config);
        }

        /// <inheritdoc/>
        public ITableColumnBuilder AddColumn(string columnTitle, ICellFormat headerFormat = null, ICellFormat rowsFormat = null)
        {
            if(headerFormat is null && _defaultHeaderFormat != null)
            {
                headerFormat = _defaultHeaderFormat;
            }

            IColumn column = new Column(columnTitle, headerFormat, rowsFormat);

            TableColumnBuilder builder = new TableColumnBuilder(column, this);
            columns.Enqueue(builder);
            return builder;
        }

        /// <inheritdoc/>
        public ITableColumnBuilder AddColumn(IColumn column)
        {
            TableColumnBuilder builder = new TableColumnBuilder(column, this);
            columns.Enqueue(builder);
            return builder;
        }

        /// <inheritdoc/>
        public Table Build()
        {
            while (columns.Any())
            {
                TableColumnBuilder builder = columns.Dequeue();
                table.AddColumn(builder.GetColumn());
            }

            return table;
        }
    }
}
