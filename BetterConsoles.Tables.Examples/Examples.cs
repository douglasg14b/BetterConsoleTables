using BetterConsoles.Colors.Extensions;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Examples
{
    public static class Examples
    {
        /// <summary>
        /// A simple, unformatted, colorless, table
        /// </summary>
        public static void Simple()
        {
            Table table = new Table("one", "two", "three");
            table.AddRow(1, 2, 3)
                 .AddRow("long line goes here", "short text", "word");

            Console.Write(table.ToString());
            Console.ReadKey();
        }

        /// <summarythe
        /// Setting the box divider/border style of teh table
        /// </summary>
        public static void SettingTableBoxStyle()
        {
            Table table;

            // During construction
            table = new Table(TableConfig.Unicode(), "one", "two", "three");

            // Setting the config property directly
            table.Config = TableConfig.UnicodeAlt();

            // Remove inner row dividers
            table.Config.hasInnerRows = false;
        }

        public static void SimpleUnicodeBox()
        {
            Table table = new Table(TableConfig.Unicode(), "one", "two", "three");
            table.AddRow(1, 2, 3)
                 .AddRow("long line goes here", "short text", "word");

            Console.Write(table.ToString());
            Console.ReadKey();
        }

        /// <summary>
        /// Headers & rows alignment can be set during table construction
        /// </summary>
        public static void SimpleAlignment()
        {
            var columns = new string[] { "one", "two", "three" };
            Table table = new Table(Alignment.Center, Alignment.Center, columns);
            table.AddRow(1, 2, 3)
                 .AddRow("long line goes here", "short text", "word");

            Console.Write(table.ToString());
            Console.ReadKey();
        }

        // NOTE: This currently erases column names & column/row formatting. THis will be improved in the future.
        public static void FromObjects()
        {
            DataStuff[] objects = new DataStuff[10];
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                objects[i] = new DataStuff(random);
            }

            Table table = new Table(TableConfig.MySql());
            table.From<DataStuff>(objects);

            Console.Write(table.ToString());
            Console.ReadKey();
        }

        /// <summary>
        /// Allows you to replace the table data with new rows, without having to re-create & re-configure the table.
        /// </summary>
        public static void ReplaceData()
        {
            Table table = new Table("one", "two", "three");
            table.AddRow(1, 2, 3)
                 .AddRow("long line goes here", "short text", "word");

            Console.Write(table.ToString());

            table.ReplaceRows(new List<object[]>()
                {
                    new [] { "123", "2", "3" },
                    new [] { "Hello World!", "item", "Here" },
                    new [] { "Replaced", "the", "data" },
                });

            Console.Write(table.ToString());
            Console.ReadKey();
        }


        public static void PreDefinedHeaders()
        {
            IColumn[] headers = new[]
            {
                new Column("One!"),
                new Column("Two!"),
                new Column("Three!"),
            };

            Table table = new Table(headers)
                .AddRow(1, 2, 3)
                .AddRow("long line goes here", "short text", "word");

            Console.Write(table.ToString());
            Console.ReadKey();
        }

        public static void PreDefinedHeadersAlt()
        {
            IColumn[] headers = new[]
            {
                new ColumnBuilder("One!").GetColumn(),
                new ColumnBuilder("Two").GetColumn(),
                new ColumnBuilder("Three").GetColumn(),
            };

            Table table = new Table(headers)
                .AddRow(1, 2, 3)
                .AddRow("long line goes here", "short text", "word");

            Console.Write(table.ToString());
            Console.ReadKey();
        }

        /// <summary>
        /// Value formatters allow you to have custom formatting callbacks for different columns.
        /// Each value in that column (excluding the header) will be passed into the formatter
        /// </summary>
        public static void ValueFormatters()
        {
            Color green = Color.FromArgb(152, 168, 75);
            Color red = Color.IndianRed;

            // Our formatter function
            string FormatMoney(double value)
            {
                string valueStr = string.Format("{0:$#.00}", value);
                valueStr = valueStr.ForegroundColor(value >= 0 ? green : red);

                return valueStr;
            }

            Table table = new TableBuilder(TableConfig.Unicode())
                .AddColumn("Date")
                .AddColumn("Money")
                    .RowFormatter<double>((val) => FormatMoney(val)) // All values in this column will be passed into the formatter
                .Build();

            table.AddRow("04/15/2018", 4678.23d)
                .AddRow("05/21/2019", -1954d)
                .AddRow("07/02/2019", 321.10d);

            Console.Write(table.ToString());
            Console.ReadKey();
        }
    }
}
