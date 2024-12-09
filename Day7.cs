namespace AdventOfCode;

public partial record Day7(string Input) : AdventDay(Input)
{

    public override void SolvePart1()
    {
        var equations = Input.Split('\n')
            .Select(Equation.Parse)
            .ToArray();

        Console.WriteLine(equations.Count(equation => equation.IsSolvable());
    }

    public override void SolvePart2()
    {

    }

}