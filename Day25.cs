namespace AdventOfCode;

public record Day25 : AdventDay<Day25>
{

    public override void SolvePart1()
    {
        var schematics = Input.Split("\n\n")
            .Select(schematic => new Schematic(schematic.Split('\n')))
            .ToList();
        
        Console.WriteLine(sum);
    }

    public override void SolvePart2() { }

}