global using Number = System.Numerics.BigInteger;

namespace AdventOfCode.Year2024;

public partial record Day21 : AdventDay<Day21>
{

    private readonly string[] _sequences;

    public Day21()
    {
        _sequences = Input.Trim().Split('\n');
        SumSequenceComplexities(1); // Preload static members outside timed section
    }

    public override void SolvePart1()
    {
        Console.WriteLine(SumSequenceComplexities(2));
    }

    public override void SolvePart2()
    {
        // Max for UInt128: 86 (complexity sum of 271892954631843287847658682840772694698 ~= 2.7e38)
        Console.WriteLine(SumSequenceComplexities(25));
    }

    private Number SumSequenceComplexities(int directionalRobotCount)
    {
        return checked(_sequences
            .Select(code => CalculateComplexity(code, directionalRobotCount))
            .Aggregate(Number.Zero, (sum, complexity) => (sum + complexity)));
    }

    private static Number CalculateComplexity(string code, int directionalRobotCount)
    {
        return Number.Parse(code[..^1]) * GetSuperSequenceLength(code, directionalRobotCount);
    }

    private static Number GetSuperSequenceLength(string code, int directionalRobotCount)
    {
        return KeyPad.Numeric.GetNthOrderSuperSequenceLength(code, directionalRobotCount);
    }

}