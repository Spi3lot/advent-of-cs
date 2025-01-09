namespace AdventOfCode;

public partial record Day21
{

    private sealed class NumericKeyPad : KeyPad
    {
        public NumericKeyPad() : base(["789", "456", "123", " 0A"])
        {

        }
        public override long GetSuperSequenceLength(string code, int intermediateRobotCount)
        {
            var deltaSequenceCounts = CountSuperSequenceDeltaSequences(code);

            return Directional.CountNthOrderSuperSequenceDeltaSequences(deltaSequenceCounts, intermediateRobotCount - 1)
                .Select(sequenceCount => sequenceCount.Key.Length * sequenceCount.Value)
                .Sum();
        }

    }

}