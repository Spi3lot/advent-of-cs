namespace AdventOfCode;

public partial record Day21 : AdventDay<Day21>
{

    private readonly string[] _sequences;

    public Day21()
    {
        _sequences = Input.Trim().Split('\n');
    }

    public override void SolvePart1()
    {
        Console.WriteLine(SumSequenceComplexities(2));
    }

    public override void SolvePart2()
    {
        Console.WriteLine(SumSequenceComplexities(25));
    }

    private long SumSequenceComplexities(int directionalRobotCount)
    {
        return _sequences.Sum(code => CalculateComplexity(code, directionalRobotCount));
    }

    private static long CalculateComplexity(string code, int directionalRobotCount)
    {
        long sequenceLength = GetSequenceLengthForTypingCode(code, directionalRobotCount);
        return sequenceLength * long.Parse(code[..^1]);
    }

    private static long GetSequenceLengthForTypingCode(string code, int directionalRobotCount)
    {
        return KeyPad.GetSuperSequenceLength(code, directionalRobotCount);
    }

}