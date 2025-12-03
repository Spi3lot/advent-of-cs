namespace AdventOfCode.Year2024;

public partial record Day25 : AdventDay<Day25>
{

    public override void SolvePart1()
    {
        var schematics = Input.Split("\n\n")
            .Select(schematic => new Schematic(schematic.Split('\n')))
            .ToList();

        var keys = schematics.Where(schematic => schematic.IsKey).ToList();
        var locks = schematics.Where(schematic => !schematic.IsKey).ToList();
        int combinations = locks.Sum(@lock => keys.Count(key => key.Fits(@lock)));
        Console.WriteLine(combinations);
    }

    public override void SolvePart2() { }

}