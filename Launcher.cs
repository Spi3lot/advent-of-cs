using System.Diagnostics;

namespace AdventOfCode;

public static class Launcher
{

    public static void Main()
    {
        var day = new Day7(File.ReadAllText("day7/input.txt"));
        SolvePartsAndPrintElapsedTimes(day);
    }

    private static void SolvePartsAndPrintElapsedTimes(AdventDay day)
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
