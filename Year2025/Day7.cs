namespace AdventOfCode.Year2025;

public partial record Day7() : AdventDay<Day7>(2025)
{

    public override void SolvePart1()
    {
        Console.WriteLine(new TachyonManifold(Input).CountSplits());
    }


    public override void SolvePart2()
    {
        Console.WriteLine(new TachyonManifold(Input).CountTimelines());
    }

}