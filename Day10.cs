namespace AdventOfCode;

public partial record Day10 : AdventDay<Day10>
{

    public override void SolvePart1()
    {
        var map = new TopographicMap(Input);
        Console.WriteLine(map);
    }

    public override void SolvePart2() { }

}