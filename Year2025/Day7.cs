namespace AdventOfCode.Year2025;

public partial record Day7() : AdventDay<Day7>(2025)
{

    public override void SolvePart1()
    {
        var manifold = new TachyonManifold(Input);
        manifold.Evaluate();
        Console.WriteLine(manifold.SplitCount);
    }


    public override void SolvePart2()
    {
    }

}