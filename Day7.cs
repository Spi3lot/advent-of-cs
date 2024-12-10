namespace AdventOfCode;

public partial record Day7 : AdventDay<Day7>
{

    public override void SolvePart1()
    {
        long totalCalibrationResult = Input.Split('\n')
            .Where(static line => !string.IsNullOrEmpty(line))
            .Select(Equation.Parse)
            .Where(static equation => equation.IsSolvable(2))
            .Select(static equation => (long) equation.Numbers[0])
            .Sum();

        Console.WriteLine(totalCalibrationResult);
    }

    public override void SolvePart2()
    {
        long totalCalibrationResult = Input.Split('\n')
            .Where(static line => !string.IsNullOrEmpty(line))
            .Select(Equation.Parse)
            .Where(static equation => equation.IsSolvable(3))
            .Select(static equation => (long) equation.Numbers[0])
            .Sum();

        Console.WriteLine(totalCalibrationResult);
    }

}