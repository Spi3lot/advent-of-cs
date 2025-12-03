using System.Diagnostics;
using AdventOfCode.Year2025;

namespace AdventOfCode;

public static class Launcher
{
   
    public static void Main()
    {
        var day = new Day1();
        SolvePartsAndPrintElapsedTimes(day);
    }

    private static void SolvePartsAndPrintElapsedTimes<T>(AdventDay<T> day) where T : AdventDay<T>
    {
        var stopWatch = Stopwatch.StartNew();
        day.SolvePart1();
        stopWatch.Stop();
        Console.WriteLine($"Part 1: {stopWatch.Elapsed.TotalSeconds:F7}s\n");

        stopWatch.Restart();
        day.SolvePart2();
        stopWatch.Stop();
        Console.WriteLine($"Part 2: {stopWatch.Elapsed.TotalSeconds:F7}s");
    }

}