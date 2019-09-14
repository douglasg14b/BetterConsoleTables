using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace BetterConsoleTablesExample
{
    // Derived from https://stackoverflow.com/a/16157458
    public class Clock
    {
        interface IStopwatch
        {
            bool IsRunning { get; }
            TimeSpan Elapsed { get; }

            void Start();
            void Stop();
            void Reset();
        }



        class TimeWatch : IStopwatch
        {
            Stopwatch stopwatch = new Stopwatch();

            public TimeSpan Elapsed => stopwatch.Elapsed;

            public bool IsRunning => stopwatch.IsRunning;



            public TimeWatch()
            {
                if (!Stopwatch.IsHighResolution)
                    throw new NotSupportedException("Your hardware doesn't support high resolution counter");

                //prevent the JIT Compiler from optimizing Fkt calls away
                long seed = Environment.TickCount;

                //use the second Core/Processor for the test
                Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);

                //prevent "Normal" Processes from interrupting Threads
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                //prevent "Normal" Threads from interrupting this thread
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
            }



            public void Start()
            {
                stopwatch.Start();
            }

            public void Stop()
            {
                stopwatch.Stop();
            }

            public void Reset()
            {
                stopwatch.Reset();
            }
        }

        class CpuWatch : IStopwatch
        {
            TimeSpan startTime;
            TimeSpan endTime;
            bool isRunning;

            public TimeSpan Elapsed
            {
                get
                {
                    if (IsRunning)
                        throw new NotImplementedException("Getting elapsed span while watch is running is not implemented");

                    return endTime - startTime;
                }
            }

            public bool IsRunning
            {
                get { return isRunning; }
            }



            public void Start()
            {
                startTime = Process.GetCurrentProcess().TotalProcessorTime;
                isRunning = true;
            }

            public void Stop()
            {
                endTime = Process.GetCurrentProcess().TotalProcessorTime;
                isRunning = false;
            }

            public void Reset()
            {
                startTime = TimeSpan.Zero;
                endTime = TimeSpan.Zero;
            }
        }

        public static void BenchmarkTime(Action action, int iterationsPerChunk = 100, int iterations = 100)
        {
            Benchmark<TimeWatch>(action, iterationsPerChunk, iterations);
        }

        static void Benchmark<T>(Action action, int iterationsPerChunk = 100, int iterations = 100) where T : IStopwatch, new()
        {
            //clean Garbage
            GC.Collect();

            //wait for the finalizer queue to empty
            GC.WaitForPendingFinalizers();

            //clean Garbage
            GC.Collect();

            //warm up
            action();

            var stopwatch = new T();
            var timings = new double[iterations];

            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Reset();
                if (i > 0)
                {
                    cursorLeft = Console.CursorLeft;
                    cursorTop = Console.CursorTop;
                }

                for (int j = 0; j < iterationsPerChunk; j++)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    stopwatch.Start();

                    action();

                    stopwatch.Stop();
                    if (i+1 % 10 == 0 || i + 1 == iterationsPerChunk)
                    {
                        Console.WriteLine($"{i + 1}/{iterationsPerChunk}");
                    }
                }


                timings[i] = stopwatch.Elapsed.Ticks;

                var chunkTime = timings[i] / 10000;
                var chunkPerIteration = Math.Truncate(chunkTime / iterationsPerChunk * 1000)/1000;
                chunkTime = Math.Truncate(chunkTime * 1000) / 1000;

                Console.WriteLine($"{i}/{iterations}: {chunkTime} ms | {chunkPerIteration} ms/iteration ");
            }
            var normalized = timings.NormalizedMean();
            var perIteration = Math.Truncate(normalized / iterationsPerChunk * 1000)/1000;
            normalized = Math.Truncate(normalized/10000 * 1000)/1000;
            perIteration = Math.Truncate(perIteration / 10000 * 1000)/1000;


            Console.WriteLine("Normalized Mean: ");
            Console.WriteLine($"    {normalized}ms/chunk");
            Console.WriteLine($"    {perIteration}ms/iteration");
        }

        public static void BenchmarkCpu(Action action, int iterations = 10000)
        {
            Benchmark<CpuWatch>(action, iterations);
        }
    }
}
