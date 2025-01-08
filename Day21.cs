namespace AdventOfCode;

public partial record Day21 : AdventDay<Day21>
{

    private static readonly KeyPad NumericKeyPad = new("789", "456", "123", " 0A");

    private static readonly KeyPad DirectionalKeyPad = new(" ^A", "<v>");

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
        Console.WriteLine(SumSequenceComplexities(19));
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
        string sequence = NumericKeyPad.GetSequenceForTyping(code);
        return DirectionalKeyPad.GetSequenceLengthForTyping(sequence, directionalRobotCount);
    }

}