namespace AdventOfCode;

public partial record Day21
{

    private sealed class DirectionalKeyPad : KeyPad
    {

        private readonly Dictionary<string, Dictionary<string, UInt128>> _superSequenceDeltaSequenceCounts = [];

        public DirectionalKeyPad() : base([" ^A", "<v>"])
        {
            foreach (string sequence in Sequences.Values.Distinct())
            {
                _superSequenceDeltaSequenceCounts[sequence] = CountSuperSequenceDeltaSequences(sequence);
            }
        }

        public override UInt128 GetNthOrderSuperSequenceLength(string sequence, int intermediateRobotCount)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(intermediateRobotCount);
            var deltaSequenceCounts = CountSuperSequenceDeltaSequences(sequence);

            return CountNthOrderSuperSequenceDeltaSequences(deltaSequenceCounts, intermediateRobotCount)
                .Select(sequenceCount => (UInt128) sequenceCount.Key.Length * sequenceCount.Value)
                .Aggregate(UInt128.Zero, (sum, current) => sum + current);
        }

        private Dictionary<string, UInt128> CountNthOrderSuperSequenceDeltaSequences(
            Dictionary<string, UInt128> deltaSequenceCounts,
            int n
        )
        {
            for (int i = 0; i < n; i++)
            {
                deltaSequenceCounts = CountSuperSequenceDeltaSequences(deltaSequenceCounts);
            }

            return deltaSequenceCounts;
        }

        private Dictionary<string, UInt128> CountSuperSequenceDeltaSequences(Dictionary<string, UInt128> sequenceCounts)
        {
            var counts = new Dictionary<string, UInt128>();

            foreach (var sequenceCount in sequenceCounts)
            {
                counts.MergeAll(
                    _superSequenceDeltaSequenceCounts[sequenceCount.Key],
                    UInt128.Zero,
                    (_, oldValue, newValue) => oldValue + newValue * sequenceCount.Value
                );
            }

            return counts;
        }

    }

}