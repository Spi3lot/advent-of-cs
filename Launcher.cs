using System.Diagnostics;

namespace AdventOfCode;

public static class Launcher
{

    public static void Main()
    {
        SolvePartsAndPrintElapsedTimes<Year2025.Day9>();
    }

    private static void SolvePartsAndPrintElapsedTimes<T>() where T : AdventDay<T>, new()
    {
        var stopWatch = Stopwatch.StartNew();
        var day = new T();
        stopWatch.Stop();
        Console.WriteLine($"ctor: {stopWatch.Elapsed.TotalSeconds:F7}s\n");

        stopWatch.Restart();
        day.SolvePart1();
        stopWatch.Stop();
        Console.WriteLine($"Part 1: {stopWatch.Elapsed.TotalSeconds:F7}s\n");

        stopWatch.Restart();
        day.SolvePart2();
        stopWatch.Stop();
        Console.WriteLine($"Part 2: {stopWatch.Elapsed.TotalSeconds:F7}s");
    }

}