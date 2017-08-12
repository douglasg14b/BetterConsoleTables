using System;
using BetterConsoleTables;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace BetterConsoleTables_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowExampleGeneratedTable();
            Console.ReadLine();
        }


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
                table.Config = TableConfiguration.Unicode();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "stuff", "stuff");

                string tableString = table.ToString();

                total += stopwatch.ElapsedTicks;
                Console.WriteLine(i);
            }
            Console.WriteLine(total/ iterations);
        }

        private static void ShowExampleTables()
        {
            Table table = new Table("One", "Two", "Three");
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "stuff");

            table.Config = TableConfiguration.Default();
            string test = String.Empty.PadRight(Console.WindowWidth, '-');
            Console.Write(test);
            Console.SetCursorPosition(0, 0);
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfiguration.Markdown();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfiguration.MySql();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfiguration.MySqlSimple();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfiguration.Unicode();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfiguration.UnicodeAlt();
            Console.Write(table.ToString());
            Console.WriteLine();
        }

        private static void ShowExmapleMultiTable()
        {
            Table table = new Table("One", "Two", "Three");
            table.Config = TableConfiguration.Default();
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "stuff");

            Table table2 = new Table("One", "Two", "Three", "Four");
            table2.Config = TableConfiguration.UnicodeAlt();
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
            Table table = new Table(TableConfiguration.MySql());
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