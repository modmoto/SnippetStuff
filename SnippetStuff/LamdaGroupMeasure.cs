using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SnippetStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            DoMeasure(100);
            DoMeasure(1000);
            DoMeasure(10000);
            DoMeasure(100000);
            DoMeasure(1000000);
            DoMeasure(10000000);
            DoMeasure(100000000);
        }

        private static void DoMeasure(int time)
        {
            var enumerable1 = Enumerable.Range(1, time).ToList();
            var enumerable2 = Enumerable.Range(1, time).ToList();

            var watch1 = Stopwatch.StartNew();
            var myThings1 = enumerable1.Select(x => CreateSomething(x)).ToList();
            watch1.Stop();

            var watch2 = Stopwatch.StartNew();
            var myThings2 = enumerable2.Select(CreateSomething).ToList();
            watch2.Stop();

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"1 ({myThings1.Count}): {watch1.ElapsedMilliseconds}");
            Console.WriteLine($"2 ({myThings2.Count}): {watch2.ElapsedMilliseconds}");

            File.AppendAllText("test.csv", $"{myThings1.Count},{watch1.ElapsedMilliseconds},{watch2.ElapsedMilliseconds}\r");
        }

        private static MyThing CreateSomething(int i)
        {
            return i > 50 ? new MyThing(i + 100) : new MyThing(i);
        }
    }

    internal class MyThing
    {
        public int I { get; }

        public MyThing(int i)
        {
            I = i;
        }
    }
}