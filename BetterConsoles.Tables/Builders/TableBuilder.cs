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
        Queue<TableColumnBuilder> columns = new Queue<TableColumnBuilder>();
        Table table;

        public TableBuilder()
        {
            table = new Table();
        }

        public TableBuilder(TableConfig config)
        {
            table = new Table(config);
        }

        /// <inheritdoc/>
        public ITableColumnBuilder AddColumn(string columnTitle)
        {
            TableColumnBuilder builder = new TableColumnBuilder(columnTitle, this);
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
