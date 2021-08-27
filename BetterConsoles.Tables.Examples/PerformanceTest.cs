using BetterConsoles.Core;
using BetterConsoles.Tables;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using Clawfoot.TestUtilities.Performance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace BetterConsoles.Tables.Examples
{
    public static class PerformanceTest
    {
        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Benchmark_SimpleTable();
            Benchmark_FormattedTable();
            Benchmark_ReplaceData();
        }

        private static void Benchmark_SimpleTable()
        {
            Clock.BenchmarkTime(() =>
            {
                Table table = new Table("One", "Two", "Three");
                table.Config = TableConfig.Unicode();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "stuff", "stuff");

                string tableString = table.ToString();
            });
        }

        private static void Benchmark_FormattedTable()
        {
            Clock.BenchmarkTime(() =>
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
            });
        }

        private static void Benchmark_ReplaceData()
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

            Clock.BenchmarkTime(() =>
            {
                table.ReplaceRows(new List<object[]>()
                {
                    new [] { "123", "2", "3" },
                    new [] { "Hello World!", "item", "Here" },
                    new [] { "Replaced", "the", "data" },
                });

                string tableString = table.ToString();
            });

        }
    }
}
