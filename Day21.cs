namespace AdventOfCode;

public partial record Day21 : AdventDay<Day21>
{

    private readonly string[] _sequences;

    public Day21()
    {
        _sequences = Input.Trim().Split('\n');
        _ = KeyPad.Numeric; // Preload static members outside timed area
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

    private UInt128 SumSequenceComplexities(int directionalRobotCount)
    {
        return checked(_sequences
            .Select(code => CalculateComplexity(code, directionalRobotCount))
            .Aggregate(UInt128.Zero, (sum, complexity) => (sum + complexity)));
    }

    private static UInt128 CalculateComplexity(string code, int directionalRobotCount)
    {
        return UInt128.Parse(code[..^1]) * GetSuperSequenceLength(code, directionalRobotCount);
    }

    private static UInt128 GetSuperSequenceLength(string code, int directionalRobotCount)
    {
        return KeyPad.Numeric.GetNthOrderSuperSequenceLength(code, directionalRobotCount);
    }

}