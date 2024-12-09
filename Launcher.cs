namespace AdventOfCode;

public static class Launcher
{

    public static void Main()
    {
        var day = new Day6(File.ReadAllText("day7/input.txt"));
        day.SolvePart1();
        day.SolvePart2();
    }

}
