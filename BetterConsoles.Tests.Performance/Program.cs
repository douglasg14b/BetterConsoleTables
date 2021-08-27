using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using System;
using System.Linq;

namespace BetterConsoles.Tests.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PerformanceComparisons>();

            Console.ReadLine();
        }


    }

    public class AllowNonOptimized : ManualConfig
    {
        public AllowNonOptimized()
        {
            WithOption(ConfigOptions.DisableOptimizationsValidator, true);

            Add(DefaultConfig.Instance.GetLoggers().ToArray()); // manual config has no loggers by default
            Add(DefaultConfig.Instance.GetExporters().ToArray()); // manual config has no exporters by default
            Add(DefaultConfig.Instance.GetColumnProviders().ToArray()); // manual config has no columns by default
        }
    }
}
