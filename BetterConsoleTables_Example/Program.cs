using System;
using BetterConsoleTables;

namespace BetterConsoleTables_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table("One", "Two", "Three");
            table.Config = TableConfiguration.Markdown();
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff", "stuff");

            string tableString = table.ToString();
            Console.Write(tableString);
            Console.ReadLine();
        }
    }
}