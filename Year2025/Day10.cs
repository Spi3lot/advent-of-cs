namespace AdventOfCode.Year2025;

public partial record Day10() : AdventDay<Day10>(2025)
{

    private Machine[]? _machines;

    public override void Setup()
    {
        _machines = Input.Trim()
            .Split('\n')
            .Select(line => new Machine(line))
            .ToArray();
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_machines!.Sum(machine => machine.DetermineMinimumButtonPresses()));
    }


    public override void SolvePart2()
    {
    }

}