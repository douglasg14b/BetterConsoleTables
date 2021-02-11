using BetterConsoleTables;
using Clawfoot.TestUtilities.Performance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace BetterConsoles.Tests.Performance
{
    public static class PerformanceTestLegacy
    {
        public static PerfTestResult Run()
        {
            Console.OutputEncoding = Encoding.UTF8;

            return Benchmark_SimpleTable();
        }

        private static PerfTestResult Benchmark_SimpleTable()
        {
            return Clock.BenchmarkTime(() =>
            {
                Table table = new Table("One", "Two", "Three");
                table.Config = TableConfiguration.Unicode();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "stuff", "stuff");

                string tableString = table.ToString();
            });
        }
    }
}
