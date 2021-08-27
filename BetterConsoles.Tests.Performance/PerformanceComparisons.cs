using BenchmarkDotNet.Attributes;
using BetterConsoles.Core;
using BetterConsoles.Tables;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Alignment = BetterConsoles.Tables.Alignment;

namespace BetterConsoles.Tests.Performance
{
    [MemoryDiagnoser]
    [Config(typeof(AllowNonOptimized))]
    public class PerformanceComparisons
    {
        Table defaultTable;

        public PerformanceComparisons()
        {
            IColumn[] columns =
            {
                    new ColumnBuilder("Colors!")
                        .HeaderFormat()
                            .ForegroundColor(Color.BlueViolet)
                        .GetColumn(),
                    new ColumnBuilder("Right")
                                    .HeaderFormat()
                                        .ForegroundColor(Color.Green)
                                        .Alignment(Alignment.Right)
                                    .GetColumn(),
                    new ColumnBuilder("Center!")
                                    .HeaderFormat()
                                        .ForegroundColor(Color.Firebrick)
                                        .Alignment(Alignment.Center)
                                        .FontStyle(FontStyleExt.Bold)
                                    .RowsFormat()
                                        .ForegroundColor(Color.DarkOliveGreen)
                                        .Alignment(Alignment.Center)
                                    .GetColumn(),
                    new ColumnBuilder("Bold & Underlined!!")
                                    .HeaderFormat()
                                        .ForegroundColor(Color.SeaShell)
                                        .Alignment(Alignment.Center)
                                        .FontStyle(FontStyleExt.Bold | FontStyleExt.Underline)
                                    .GetColumn()
                };

            Table table = new Table()
                .AddColumn(columns[0])
                .AddColumn(columns[1])
                .AddColumn(columns[2])
                .AddColumn(columns[3]);
            table.Config = TableConfig.MySqlSimple();
            table.AddRow("99", "2", "3");
            table.AddRow("Hello World!", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

            this.defaultTable = table;
        }


        [Benchmark(Baseline = true)]
        public void OtherConsoleTables()
        {
            var table = new ConsoleTable("One", "Two", "Three");
            table.AddRow("1", "2", "3")
                .AddRow("Short", "item", "Here")
                .AddRow("Longer items go here", "stuff", "stuff");

            string tableString = table.ToMarkDownString();
        }

        [Benchmark]
        public void v1()
        {
            BetterConsoleTables.Table table = new BetterConsoleTables.Table("One", "Two", "Three");
            table.Config = BetterConsoleTables.TableConfiguration.Unicode();
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff", "stuff");

            string tableString = table.ToString();
        }

        [Benchmark]
        public void v2()
        {
            Table table = new Table("One", "Two", "Three");
            table.Config = TableConfig.Unicode();
            table.AddRow("1", "2", "3")
                .AddRow("Short", "item", "Here")
                .AddRow("Longer items go here", "stuff", "stuff");

            string tableString = table.ToString();
        }

        [Benchmark]
        public void v2_Formatted()
        {
            IColumn[] columns =
            {
                    new ColumnBuilder("Colors!")
                        .HeaderFormat()
                            .ForegroundColor(Color.BlueViolet)
                        .GetColumn(),
                    new ColumnBuilder("Right")
                                    .HeaderFormat()
                                        .ForegroundColor(Color.Green)
                                        .Alignment(Alignment.Right)
                                    .GetColumn(),
                    new ColumnBuilder("Center!")
                                    .HeaderFormat()
                                        .ForegroundColor(Color.Firebrick)
                                        .Alignment(Alignment.Center)
                                        .FontStyle(FontStyleExt.Bold)
                                    .RowsFormat()
                                        .ForegroundColor(Color.DarkOliveGreen)
                                        .Alignment(Alignment.Center)
                                    .GetColumn(),
                    new ColumnBuilder("Bold & Underlined!!")
                                    .HeaderFormat()
                                        .ForegroundColor(Color.SeaShell)
                                        .Alignment(Alignment.Center)
                                        .FontStyle(FontStyleExt.Bold | FontStyleExt.Underline)
                                    .GetColumn()
                };

            Table table = new Table()
                .AddColumn(columns[0])
                .AddColumn(columns[1])
                .AddColumn(columns[2])
                .AddColumn(columns[3]);
            table.Config = TableConfig.MySqlSimple();
            table.AddRow("99", "2", "3");
            table.AddRow("Hello World!", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

            string tableString = table.ToString();
        }

        [Benchmark]
        public void v2_Formatted_ReplaceData()
        {
            defaultTable.ReplaceRows(new List<object[]>()
                {
                    new [] { "123", "2", "3" },
                    new [] { "Hello World!", "item", "Here" },
                    new [] { "Replaced", "the", "data" },
                });

            string tableString = defaultTable.ToString();
        }
    }
}
