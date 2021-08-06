using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Examples
{
    public static class Examples
    {
        public static void Simple()
        {
            Table table = new Table("one", "two", "three");
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


        public static void PreDefinedHeaders1()
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

        public static void PreDefinedHeaders2()
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
    }
}
