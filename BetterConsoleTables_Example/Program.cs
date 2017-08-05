using System;
using BetterConsoleTables;
using System.Diagnostics;
using System.Text;

namespace BetterConsoleTables_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RunPerformanceTest();
            /*Table table = new Table("One", "Two", "Three", "Four");
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff", "stuff");

            table.Config = TableConfiguration.Default();
            Console.Write(table.ToString());
            Console.WriteLine();*/
            Console.ReadLine();
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
            table.AddRow("Longer items go here", "stuff", "stuff");

            table.Config = TableConfiguration.Default();
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
    }
}