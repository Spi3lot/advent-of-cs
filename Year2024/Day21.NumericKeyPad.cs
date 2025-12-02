namespace AdventOfCode.Year2024;

public partial record Day21
{

    private sealed class NumericKeyPad() : KeyPad(["789", "456", "123", " 0A"])
    {

        public override Number GetNthOrderSuperSequenceLength(string sequence, int intermediateRobotCount)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(intermediateRobotCount);
            string superSequence = GetSuperSequence(sequence);
            return Directional.GetNthOrderSuperSequenceLength(superSequence, intermediateRobotCount - 1);
        }

    }

}