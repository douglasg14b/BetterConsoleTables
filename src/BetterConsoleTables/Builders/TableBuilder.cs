using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders
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


        public ITableColumnBuilder WithColumn(string columnTitle)
        {
            TableColumnBuilder builder = new TableColumnBuilder(columnTitle, this);
            columns.Enqueue(builder);
            return builder;
        }

        public ITableColumnBuilder WithColumn(IColumn column)
        {
            TableColumnBuilder builder = new TableColumnBuilder(column, this);
            columns.Enqueue(builder);
            return builder;
        }

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
