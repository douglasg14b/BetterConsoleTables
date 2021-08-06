using System;
using BetterConsoles.Tables;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using BetterConsoles.Tables.Examples;
using BetterConsoles.Tables.Models;
using BetterConsoles.Tables.Configuration;
using System.Drawing;
using BetterConsoles.Tables.Builders;
using System.Linq;
using BetterConsoles.Core;
using BetterConsoles.Colors.Extensions;

using Clawfoot.TestUtilities.Performance;
using BetterConsoles.Tables.Builders.Interfaces;
using BetterConsoles.Colors.Builders;
using BetterConsoles.Colors;
using BetterConsoles.Tables.Common;

namespace BetterConsoles.Tables.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowPrettyTable();
            ShowBudgetTable();

            //ShowExampleGeneratedTable();
            //ShowAlignedTables();
            //ShowExmapleMultiTable();
            ShowFormattedTable();
            //ShowExampleTables();
            Console.WriteLine("Complete");
            Console.ReadLine();
        }

        //Unused TEST
        private static void RunWrapPerformanceTest1()
        {
            int iterations = 25000;
            Stopwatch stopwatch = new Stopwatch();
            long total = 0;

            for(int i = 0; i < iterations; i++)
            {
                Console.SetCursorPosition(0, 0);
                stopwatch.Restart();

                WordWrap("For a simple concatenation of 3 or 4 strings, it probably won't make any significant difference, and string concatenation may even be slightly faster - but if you're wrong and there are lots of rows, StringBuilder will start getting much more efficient, and it's always more descriptive of what you're doing.", 20);

                stopwatch.Stop();
                total += stopwatch.ElapsedTicks;
                Console.Write(i);
            }

            Console.WriteLine();
            Console.WriteLine(total / iterations);
        }

        private static void ShowBudgetTable()
        {
            var transactions = new []
            {
                new { date = "09/14/2020", payee = "Costco", category = "Shopping/Supplies", value = -327.19 },
                new { date = "07/06/2020", payee = "Starbucks", category = "Food/Coffee", value = -5.12 },
                new { date = "06/25/2020", payee = "Acme Inc", category = "Inflow", value = 2196.36 },
                new { date = "06/22/2020", payee = "Comcast", category = "Monthly/Internet", value = -97.47 }
            };

            Color positiveMoney = Color.FromArgb(152, 168, 75);
            Color negativeMoney = Color.FromArgb(204, 83, 78);
            string FormatMoney(double money)
            {
                string valueStr = string.Format("{0:$#.00}", money);

                if (money >= 0)
                {
                    valueStr = valueStr.ForegroundColor(positiveMoney);
                }
                else
                {
                    valueStr = valueStr.ForegroundColor(negativeMoney);
                }

                return valueStr;
            }


            CellFormat headerFormat = new CellFormat()
            {
                Alignment = Alignment.Center,
                ForegroundColor = Color.FromArgb(152, 114, 159)
            };



            Table table = new TableBuilder(headerFormat)
                .AddColumn("Date", rowsFormat: new CellFormat(foregroundColor: Color.FromArgb(128, 129, 126)))
                .AddColumn("Payee")
                    .RowsFormat()
                        .ForegroundColor(Color.FromArgb(100, 160, 179))
                .AddColumn("Category")
                    .RowsFormat()
                        .ForegroundColor(Color.FromArgb(100, 160, 179))
                .AddColumn("Value")
                    .RowFormatter((x) => FormatMoney((double)x))
                    .RowsFormat()
                        .Alignment(Alignment.Right)
                .Build();
            table.Config = TableConfig.Unicode();

            foreach (var item in transactions)
            {
                //var columns = new ICell[]
                //{
                //    new Cell(item.date),
                //    new Cell(item.payee),
                //    new Cell(item.category),
                //    new Cell<double>(item.value, (x) => FormatMoney(x)),
                //};

                //table.AddRow(columns);

                table.AddRow(item.date, item.payee, item.category, item.value);              
            }

            Console.Write(table.ToString());
        }

        private static void ShowPrettyTable()
        {
            CellFormat headerFormat = new CellFormat()
            {
                Alignment = Alignment.Center,
                ForegroundColor = Color.FromArgb(152, 114, 159)
            };

            Table table = new TableBuilder(headerFormat)
                .AddColumn("Date", rowsFormat: new CellFormat(foregroundColor: Color.FromArgb(128, 129, 126)))
                .AddColumn("Title")
                    .RowsFormat()
                        .ForegroundColor(Color.FromArgb(220, 220, 220))
                        .Alignment(Alignment.Left)
                        .HasInnerFormatting()
                .AddColumn("Production Budget")
                    .RowsFormat()
                        .ForegroundColor(Color.FromArgb(204, 83, 78))
                        .Alignment(Alignment.Right)
                .AddColumn("Box Office")
                    .RowsFormat()
                        .ForegroundColor(Color.FromArgb(152, 168, 75))
                        .Alignment(Alignment.Right)
                .Build();

            var soloFormatted = "Solo".ForegroundColor(Color.FromArgb(204, 83, 78));
            soloFormatted += ": A Star Wars Story".ForegroundColor(Color.FromArgb(220, 220, 220));

            table.Config = TableConfig.Unicode();
            table.AddRow("Dec 20, 2019", "Star Wars: The Rise of Skywalker", "$275,000,000", "$375,126,118")
                    .AddRow("May 25, 2018", soloFormatted, "$275,000,000", "$393,151,347")
                    .AddRow("Dec 15, 2017", "Star Wars Ep. VIII: The Last Jedi", "$262,000,000", "$1,332,539,889")
                    .AddRow(new Cell[]
                    {
                        new Cell("Dec 15, 2017"),
                        new Cell("Star Wars Ep. VIII: The Last Jedi"),
                        new Cell("$262,000,000"),
                        new Cell("$1,332,539,889", new CellFormat() { FontStyle = FontStyleExt.Underline }),
                    });


            Console.Write(table.ToString());
        }

        public static List<string> WordWrap(string input, int maxCharacters)
        {
            List<string> lines = new List<string>();

            if (!input.Contains(" "))
            {
                int start = 0;
                while (start < input.Length)
                {
                    lines.Add(input.Substring(start, Math.Min(maxCharacters, input.Length - start)));
                    start += maxCharacters;
                }
            }
            else
            {
                string[] words = input.Split(' ');

                string line = "";
                foreach (string word in words)
                {
                    if ((line + word).Length > maxCharacters)
                    {
                        lines.Add(line.Trim());
                        line = "";
                    }

                    line += string.Format("{0} ", word);
                }

                if (line.Length > 0)
                {
                    lines.Add(line.Trim());
                }
            }

            return lines;
        }
        private static void ShowFormattedTable()
        {
            Console.WriteLine();
            Table1();
            Console.WriteLine();
            Table2();

            void Table1()
            {
                //THis throws Exception
                IColumn[] headers = new[]
                {
                    new ColumnBuilder("Colors!").HeaderFormat().ForegroundColor(Color.BlueViolet).GetColumn(),
                    new ColumnBuilder("Right").HeaderFormat().Alignment(Alignment.Right).ForegroundColor(Color.Green).GetColumn(),
                    new ColumnBuilder("Center!").HeaderFormat().Alignment(Alignment.Center).ForegroundColor(Color.Firebrick).GetColumn(),
                };

                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow(Color.Gray.ToString(), "2", "3");
                table.AddRow("Hello", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());
            }

            /*ITableBuilder test = null;

            test
                .WithColumn("sdfsdf")
                    .WithHeaderFormat()
                        .WithFontStyle(FontStyleExt.Bold)*/

            void Table2()
            {

                Table table = new TableBuilder()
                    .AddColumn("Colors!")
                        .HeaderFormat()
                            .ForegroundColor(Color.BlueViolet)
                    .AddColumn("Right")
                        .HeaderFormat()
                            .ForegroundColor(Color.Green)
                            .Alignment(Alignment.Right)
                        .RowsFormat()
                            .Alignment(Alignment.Right)
                            .BackgroundColor(Color.ForestGreen)
                            .ForegroundColor(Color.DarkGray)
                    .AddColumn("Center!")
                        .HeaderFormat()
                            .ForegroundColor(Color.Firebrick)
                            .Alignment(Alignment.Center)
                            .FontStyle(FontStyleExt.Bold)
                        .RowsFormat()
                            .ForegroundColor(Color.DarkOliveGreen)
                            .Alignment(Alignment.Center)
                            .FontStyle(FontStyleExt.Bold)
                    .AddColumn("Bold & Underlined!!")
                        .HeaderFormat()
                            .ForegroundColor(Color.SeaShell)
                            .Alignment(Alignment.Center)
                            .FontStyle(FontStyleExt.Bold | FontStyleExt.Underline)
                    .Build();

                table.Config = TableConfig.MySqlSimple();
                table.AddRow("99", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());

                table.ReplaceRows(new List<object[]>()
                {
                    new [] { "123", "2", "3" },
                    new [] { "Hello World!", "item", "Here" },
                    new [] { "Replaced", "the", "data" },
                });

                Console.WriteLine();
                Console.Write(table.ToString());
            }
        }

        private static void ShowAlignedTables()
        {
            Console.WriteLine();
            Table1();
            Console.WriteLine();
            Table2();
            Console.WriteLine();

            void Table1()
            {
                IColumn[] headers = new[]
                {
                    Column.Simple("Left"),
                    Column.Simple("Right", Alignment.Right, Alignment.Right),
                    Column.Simple("Center", Alignment.Center, Alignment.Center),
                };

                var test = Color.Gray;

                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow("1", "2", "3");
                table.AddRow("Hello There How Do You", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());
            }

            void Table2()
            {
                IColumn[] headers = new[]
                {
                    Column.Simple("Left"),
                    Column.Simple("Left Header", Alignment.Right),
                    Column.Simple("Right Header", Alignment.Center, Alignment.Right),
                };
                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "Right Contents", "Centered Contents");

                Console.Write(table.ToString());
            }
        }

        private static void ShowExampleTables()
        {
            ShowFormattedTable();
            ShowAlignedTables();
            Console.WriteLine();

            Table table = new Table("One", "Two", "Three");
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "stuff");

            table.Config = TableConfig.Default();

            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.Markdown();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.MySql();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.MySqlSimple();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.Unicode();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.UnicodeAlt();
            Console.Write(table.ToString());
            Console.WriteLine();
        }

        private static void ShowExmapleMultiTable()
        {
            Table table = new Table("One", "Two", "Three");
            table.Config = TableConfig.UnicodeAlt();
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "stuff");

            Table table2 = new Table("One", "Two", "Three", "Four");
            table2.Config = TableConfig.UnicodeAlt();
            table2.AddRow("One", "Two", "Three");
            table2.AddRow("Short", "item", "Here", "A fourth column!!!");
            table2.AddRow("stuff", "longer stuff", "even longer stuff in this cell");


            Table table3 = new Table("One", "Two");
            table3.Config = TableConfig.UnicodeAlt();
            table3.AddRow("One", "Two");
            table3.AddRow("Short", "item");
            table3.AddRow("stuff", "longer stuff");

            ConsoleTables tables = new ConsoleTables(table, table2, table3);
            Console.Write(tables.ToString());
        }

        private static void ShowExampleGeneratedTable()
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
        }
    }

    public class DataStuff
    {
        public DataStuff(Random random)
        {
            Name = $"Name #{random.Next(1, 100)}";
            Count = random.Next(1000, 50000);
            TimeSpent = new TimeSpan(random.Next(123456, 1234567890));
        }

        string Name { get; set; }
        int Count { get; set; }
        TimeSpan TimeSpent { get; set; }
    }
}