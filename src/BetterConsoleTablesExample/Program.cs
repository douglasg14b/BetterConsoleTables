using System;
using BetterConsoleTables;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using BetterConsoleTablesExample;
using BetterConsoleTables.Models;
using BetterConsoleTables.Configuration;
using System.Drawing;
using BetterConsoleTables.Builders;

namespace BetterConsoleTables_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //ShowAlignedTables();
            //PerformanceTest.Run();
            ShowExampleTables();
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

        private static void RunWrapPerformanceTest()
        {
            int iterations = 25000;
            Stopwatch stopwatch = new Stopwatch();
            long total = 0;

            for (int j = 0; j < iterations; j++)
            {
                Console.SetCursorPosition(0, 0);
                stopwatch.Restart();

                int limit = 20;
                string text = "For a simple concatenation of 3 or 4 strings, it probably won't make_any_significant_difference,_and_string concatenation may even be slightly faster - but if you're wrong and there are lots of rows, StringBuilder will start getting much more efficient, and it's always more descriptive of what you're doing.";
                StringBuilder builder = new StringBuilder();

                int lastsplit = 0;
                int lastWhiteSpace = 0;
                bool lastSplitOnSpace = false;

                for (int i = 0; i < text.Length; i++)
                {
                    if (Char.IsWhiteSpace(text[i]))
                    {
                        if (!(i - lastsplit < limit && i < text.Length))
                        {
                            if (i - lastsplit == limit)
                            {
                                if (builder.Length == 0)
                                {
                                    builder.AppendLine(text.Substring(lastsplit, i - lastsplit));
                                }
                                else
                                {
                                    builder.AppendLine(text.Substring(lastsplit + 1, i - lastsplit - 1));
                                }

                                lastsplit = i;
                                lastWhiteSpace = i;
                                lastSplitOnSpace = true;
                            }
                            //Current length is over limit, new whitespace found, size of next split area is less than limit, then split on last found white space
                            else if (i - lastsplit > limit && lastsplit != lastWhiteSpace && lastWhiteSpace - lastsplit - 1 <= limit)
                            {
                                if (builder.Length == 0)
                                {
                                    builder.AppendLine(text.Substring(lastsplit, lastWhiteSpace - lastsplit));
                                }
                                else
                                {
                                    builder.AppendLine(text.Substring(lastsplit + 1, lastWhiteSpace - lastsplit - 1));
                                }
                                lastsplit = lastWhiteSpace; //Split was performed at the last whitepsace
                                lastWhiteSpace = i; //On a new whitespace right now, set that accordingly
                                lastSplitOnSpace = true;
                            }
                            //Last whitespace and last split are in the same location, and text is longer than limit. Means single word is longer than limit, then split inside word at limit
                            else
                            {
                                if (Char.IsWhiteSpace(text[lastsplit])) //Last split was a whitespace, skip forward 1 char to skip whitespace
                                {
                                    builder.AppendLine(text.Substring(lastsplit + 1, limit));
                                    lastsplit += limit + 1;
                                }
                                else
                                {
                                    builder.AppendLine(text.Substring(lastsplit, limit));
                                    lastsplit += limit;
                                }
                                lastWhiteSpace = i; //On a new whitespace right now, set that accordingly
                                lastSplitOnSpace = false;
                                continue;
                            }
                        }
                        else
                        {
                            lastWhiteSpace = i;
                        }

                        if (i + 1 != text.Length && Char.IsWhiteSpace(text[i + 1])) //If next char is whitespace, move forward till no more white space
                        {
                            i++;
                            for (; i < text.Length; i++)
                            {
                                if (Char.IsWhiteSpace(text[i]))
                                {
                                    continue;
                                }
                                else
                                {
                                    i--; //Current character isn't whitespace, go back a character
                                    lastWhiteSpace = i;
                                    lastsplit = i;
                                    break;
                                }
                            }
                        }
                    }

                    if (i + 1 == text.Length)
                    {
                        if (lastSplitOnSpace) //split was done on a space, skip forward one to skip excess space
                        {
                            builder.AppendLine(text.Substring(lastsplit + 1, i - lastsplit));
                        }
                        else //Split wasn't done on a space
                        {
                            builder.AppendLine(text.Substring(lastsplit, i - lastsplit + 1));
                        }
                    }
                }
                string output = builder.ToString();
                stopwatch.Stop();
                total += stopwatch.ElapsedTicks;
                Console.Write(j);
            }

            Console.WriteLine();
            Console.WriteLine(total / iterations);
        }

        private static void RunPerformanceTest()
        {
            int iterations = 25000;
            Console.OutputEncoding = Encoding.UTF8;
            Stopwatch stopwatch = new Stopwatch();
            long total = 0;
            for(int i = 0; i < iterations; i++)
            {
                Console.SetCursorPosition(0, 0);
                stopwatch.Restart();

                Table table = new Table("One", "Two", "Three");
                table.Config = TableConfig.Unicode();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "stuff", "stuff");

                string tableString = table.ToString();

                total += stopwatch.Elapsed.Ticks;
                if(i % 100 == 0 || i+1 == iterations)
                {
                    Console.WriteLine(i+1);
                }              
            }

            long ticks = total / iterations;
            Console.WriteLine();
            Console.WriteLine($"{ticks}ticks or {ticks/10000f}ms per table");
        }

        private static void ShowColoredHeaders()
        {
            Console.WriteLine();
            Table1();
            Console.WriteLine();
            Table2();

            void Table1()
            {
                //THis throws Exception
                return;
                Column[] headers = new[]
                {
                    new ColumnBuilder("Colors!").WithHeaderFormat().WithForegroundColor(Color.BlueViolet).GetColumn(),
                    new ColumnBuilder("Right").WithHeaderFormat().WithForegroundColor(Color.Green).GetColumn(),
                    new ColumnBuilder("Center!").WithHeaderFormat().WithForegroundColor(Color.Firebrick).GetColumn(),
                };

                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow(Color.Gray.ToString(), "2", "3");
                table.AddRow("Hello", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());
            }

            void Table2()
            {
                Table table = new Table()
                    .AddColumn(new ColumnBuilder("Colors!").WithHeaderFormat().WithForegroundColor(Color.BlueViolet).GetColumn())
                    .AddColumn(new ColumnBuilder("Right")
                                    .WithHeaderFormat()
                                        .WithForegroundColor(Color.Green)
                                        .WithAlignment(Alignment.Right)
                                    .GetColumn())
                    .AddColumn(new ColumnBuilder("Center!")
                                    .WithHeaderFormat()
                                        .WithForegroundColor(Color.Firebrick)
                                        .WithAlignment(Alignment.Center)
                                    .GetColumn());

                table.Config = TableConfig.MySqlSimple();
                table.AddRow(Color.Gray.ToString(), "2", "3");
                table.AddRow("Hello", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

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
                Column[] headers = new[]
                {
                    Column.Simple("Left"),
                    Column.Simple("Right", Alignment.Right, Alignment.Right),
                    Column.Simple("Center", Alignment.Center, Alignment.Center),
                };

                var test = Color.Gray;

                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow(Color.Gray.ToString(), "2", "3");
                table.AddRow("Hello".WithColor(Color.LightGray, ColorPlane.Foreground), "2".WithColor(Color.Red, ColorPlane.Foreground), "3");
                table.AddRow("\u001b[48;2;55;90;150mHello World!\u001b[0m", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());
            }

            void Table2()
            {
                Column[] headers = new[]
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
            ShowColoredHeaders();
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
            table.Config = TableConfig.Default();
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "stuff");

            Table table2 = new Table("One", "Two", "Three", "Four");
            table2.Config = TableConfig.UnicodeAlt();
            table2.AddRow("One", "Two", "Three");
            table2.AddRow("Short", "item", "Here", "A fourth column!!!");
            table2.AddRow("stuff", "longer stuff", "even longer stuff in this cell");

            ConsoleTables tables = new ConsoleTables(table, table2);
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