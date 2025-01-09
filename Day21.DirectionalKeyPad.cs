using System.Text;

namespace AdventOfCode;

public partial record Day21
{

    private sealed class DirectionalKeyPad : KeyPad
    {

        private readonly Dictionary<string, Dictionary<string, long>> _superSequenceDeltaSequenceCounts = [];

        public DirectionalKeyPad() : base([" ^A", "<v>"])
        {

            foreach (string sequence in _sequences.Values)
            {
                Console.WriteLine(_superSequenceDeltaSequenceCounts.ContainsKey(sequence));
                _superSequenceDeltaSequenceCounts[sequence] = CountSuperSequenceDeltaSequences(sequence);
            }
        }

        public override long GetSuperSequenceLength(string sequence, int intermediateRobotCount)
        {
            var deltaSequenceCounts = CountSuperSequenceDeltaSequences(sequence);

            return CountNthOrderSuperSequenceDeltaSequences(deltaSequenceCounts, intermediateRobotCount)
                .Select(sequenceCount => sequenceCount.Key.Length * sequenceCount.Value)
                .Sum();
        }

        public Dictionary<string, long> CountNthOrderSuperSequenceDeltaSequences(
            Dictionary<string, long> deltaSequenceCounts,
            int n
        )
        {
            for (int i = 0; i < n; i++)
            {
                deltaSequenceCounts = CountSuperSequenceDeltaSequences(deltaSequenceCounts);
            }

            return deltaSequenceCounts;
        }

        public Dictionary<string, long> CountSuperSequenceDeltaSequences(Dictionary<string, long> sequenceCounts)
        {
            var counts = new Dictionary<string, long>();

            foreach (var sequenceCount in sequenceCounts)
            {
                counts.MergeAll(
                    _superSequenceDeltaSequenceCounts[sequenceCount.Key],
                    (key, oldValue, newValue) => oldValue + newValue * sequenceCount.Value
                );
            }

            return counts;
        }

        public string Press(string sequence)
        {
            var stringBuilder = new StringBuilder(sequence.Count(key => key == 'A'));
            var (x, y) = _positions['A'];

            foreach (char key in sequence)
            {
                _ = key switch
                {
                    'A' => stringBuilder.Append(_layout[y][x]).Length, // .Length just so that an int is returned... ugly but better than useless delegate allocations
                    '<' => x--,
                    '>' => x++,
                    '^' => y--,
                    'v' => y++,
                    _ => throw new ArgumentException($"Sequence contains an invalid character: {key}"),
                };
            }

            return stringBuilder.ToString();
        }

    }

}