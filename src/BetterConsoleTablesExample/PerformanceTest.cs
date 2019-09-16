using BetterConsoleTables;
using BetterConsoleTables.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace BetterConsoleTablesExample
{
    public static class PerformanceTest
    {
        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Clock.BenchmarkTime(() =>
            {
                Table table = new Table("One", "Two", "Three");
                table.Config = TableConfig.Unicode();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "stuff", "stuff");

                string tableString = table.ToString();
            }, 100, 500);
        }

        public static double NormalizedMean(this ICollection<double> values)
        {
            if (values.Count == 0)
                return double.NaN;

            var deviations = values.Deviations().ToArray();
            var meanDeviation = deviations.Sum(t => Math.Abs(t.Item2)) / values.Count;
            return deviations.Where(t => t.Item2 > 0 || Math.Abs(t.Item2) <= meanDeviation).Average(t => t.Item1);
        }

        public static IEnumerable<Tuple<double, double>> Deviations(this ICollection<double> values)
        {
            if (values.Count == 0)
                yield break;

            var avg = values.Average();
            foreach (var d in values)
                yield return Tuple.Create(d, avg - d);
        }
    }
}
