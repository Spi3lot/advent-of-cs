namespace AdventOfCode.Year2024;

public partial record Day21
{

    private sealed class DirectionalKeyPad : KeyPad
    {

        private readonly Dictionary<string, Dictionary<string, Number>> _superSequenceDeltaSequenceCounts = [];

        public DirectionalKeyPad() : base([" ^A", "<v>"])
        {
            foreach (string sequence in Sequences.Values.Distinct())
            {
                _superSequenceDeltaSequenceCounts[sequence] = CountSuperSequenceDeltaSequences(sequence);
            }
        }

        public override Number GetNthOrderSuperSequenceLength(string sequence, int intermediateRobotCount)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(intermediateRobotCount);
            var deltaSequenceCounts = CountSuperSequenceDeltaSequences(sequence);

            return CountNthOrderSuperSequenceDeltaSequences(deltaSequenceCounts, intermediateRobotCount)
                .Select(sequenceCount => (Number) sequenceCount.Key.Length * sequenceCount.Value)
                .Aggregate(Number.Zero, (sum, current) => sum + current);
        }

        private Dictionary<string, Number> CountNthOrderSuperSequenceDeltaSequences(
            Dictionary<string, Number> deltaSequenceCounts,
            int n
        )
        {
            for (int i = 0; i < n; i++)
            {
                deltaSequenceCounts = CountSuperSequenceDeltaSequences(deltaSequenceCounts);
            }

            return deltaSequenceCounts;
        }

        private Dictionary<string, Number> CountSuperSequenceDeltaSequences(Dictionary<string, Number> sequenceCounts)
        {
            var counts = new Dictionary<string, Number>();

            foreach (var sequenceCount in sequenceCounts)
            {
                counts.MergeAll(
                    _superSequenceDeltaSequenceCounts[sequenceCount.Key],
                    Number.Zero,
                    (_, oldValue, newValue) => oldValue + newValue * sequenceCount.Value
                );
            }

            return counts;
        }

    }

}