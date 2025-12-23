namespace AdventOfCode.Year2025;

public partial record Day10 : AdventDay<Day10>
{

    private readonly Machine[] _machines;

    public Day10() : base(2025)
    {
        _machines = Input.Trim()
            .Split('\n')
            .Select(line => new Machine(line))
            .ToArray();
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_machines.Sum(machine => machine.DetermineMinimumButtonPressCount(false)));
    }


    public override void SolvePart2()
    {
        int sum = _machines.AsParallel()
            .Select(machine => machine.DetermineMinimumButtonPressCount(true))
            .LazyForAll((count, index) => Console.WriteLine($"{index}: {count}"))
            .Sum();

        Console.WriteLine(sum);
    }

}