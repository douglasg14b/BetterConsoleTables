using System;
using BetterConsoleTables;
using System.Diagnostics;

namespace BetterConsoleTables_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            long total = 0;
            for(int i = 0; i < 100000; i++)
            {
                Console.SetCursorPosition(0, 0);
                stopwatch.Restart();

                Table table = new Table("One", "Two", "Three");
                table.Config = TableConfiguration.MySql();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "stuff", "stuff");

                string tableString = table.ToString();

                total += stopwatch.ElapsedTicks;
                Console.WriteLine(i);
            }
            Console.WriteLine(total/100000);

            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //stopwatch.Restart();

            //string tableString = table.ToString();
            //Console.Write(tableString);
            //Console.Write(stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}