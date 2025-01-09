using System.Text;

namespace AdventOfCode;

public partial record Day21
{

    private abstract class KeyPad
    {
        
        public static readonly NumericKeyPad Numeric = new();
        
        public static readonly DirectionalKeyPad Directional = new();

        protected readonly Dictionary<(char From, char To), string> Sequences = [];

        private readonly Dictionary<char, (int X, int Y)> _positions = [];

        private readonly (int X, int Y) _gapPosition;

        private readonly string[] _layout;

        protected KeyPad(string[] layout)
        {
            _layout = layout;

            foreach (var (j, line) in layout.Index())
            {
                foreach (var (i, key) in line.Index())
                {
                    _positions[key] = (i, j);
                }
            }

            _gapPosition = _positions[' '];
            _positions.Remove(' ');

            foreach (var from in _positions)
            {
                foreach (var to in _positions)
                {
                    Sequences[(from.Key, to.Key)] = GetDeltaSequence(from.Value, to.Value);
                }
            }
        }

        // ALWAYS TRAVEL FURTHEST (<) FIRST
        // AND ^ BEFORE > BECAUSE ^ IS < OF A
        //                    BUT > IS v OF A
        // 
        // <v   A
        // v<<A>A   ^>A
        // <vA<AA>>^AvA^A   <Av>A^A
        // 
        // is better than
        // 
        // v<   A
        // <vA<A>>   ^A
        // v<<A>A^>Av<<A>>^AvAA   <^A>A
        // 
        // and even better (judging by applied rules rather than sequence length) than
        // 
        // v<   A
        // v<A<A   >>^A
        // v<A<A>>^Av<<A>>^A   vAA^<A>A
        // 
        // 
        // 
        // v>   A
        // <vA>A  ^A
        // v<<A>A^>AvA^A  <A>A
        // 
        // is better than
        // 
        // >v   A
        // vA<A   >^A
        // v<A>^Av<<A>>^A   vA^<A>A 
        // 
        // 
        // 
        // ^>   A
        // <Av>   A   ^A
        // v<<A>>^A<vA>   A   ^A   <A>A
        // <vA<AA>>^AvAA<^A>Av<<A>A^>Av   A   ^A   <A>A   v<<A>>^AvA^A
        //  v<<A>A^>Av<<A>>^AAvAA<^A>A<vA^>AAv<<A>^A>AvA^A<vA<AA>>^AvA^A<Av>A^A<v   A   ^>A   <A>A   v<<A>>^AvA^A   <vA<AA>>^AvAA<^A>A<vA^>A<A>A
        // 
        // is better than
        // 
        // >^   A
        // vA<^   A   >A
        // <vA>^Av<<A>^   A   >A   vA^A
        // v<<A>A>^AvA<^A>A<vA<AA>>^AvA   <^A   >A   vA^A   <vA>^A<A>A
        //  <vA<AA>>^AvA^AvA<^A>A<vA>^Av<<A>^A>AvA^Av<<A>A>^Av<<A>>^AAvAA<^A>A<vA>^   A   v<<A>^A>A   vA^A   <vA>^A<A>A   v<<A>A>^AvA<^A>Av<<A>>^AvA^A
        private string GetDeltaSequence((int X, int Y) from, (int X, int Y) to)
        {
            (int X, int Y) delta = (to.X - from.X, to.Y - from.Y);
            var stringBuilder = new StringBuilder(Math.Abs(delta.X) + Math.Abs(delta.Y) + 1);

            if (from.X == _gapPosition.X && to.Y == _gapPosition.Y)
            {
                AppendHorizontal(delta.X, stringBuilder);
                AppendVertical(delta.Y, stringBuilder);
            }
            else if (from.Y == _gapPosition.Y && to.X == _gapPosition.X)
            {
                AppendVertical(delta.Y, stringBuilder);
                AppendHorizontal(delta.X, stringBuilder);
            }
            else
            {
                _ = delta switch
                {
                    (0, 0) => stringBuilder,
                    (not 0, 0) => AppendHorizontal(delta.X, stringBuilder),
                    (0, not 0) => AppendVertical(delta.Y, stringBuilder),
                    ( < 0, < 0) => stringBuilder.Append('<', -delta.X).Append('^', -delta.Y), // ^ is closer to A than <
                    ( < 0, > 0) => stringBuilder.Append('<', -delta.X).Append('v', delta.Y), // v is closer to A than <
                    ( > 0, < 0) => stringBuilder.Append('^', -delta.Y).Append('>', delta.X), // > is as close to A as ^
                    ( > 0, > 0) => stringBuilder.Append('v', delta.Y).Append('>', delta.X), // > is closer to A than v
                };
            }

            return stringBuilder.Append('A').ToString();
        }

        public string GetSuperSequence(string sequence)
        {
            var stringBuilder = new StringBuilder(sequence.Length);
            char previousKey = 'A';

            foreach (char key in sequence)
            {
                stringBuilder.Append(Sequences[(previousKey, key)]);
                previousKey = key;
            }

            return stringBuilder.ToString();
        }

        public abstract UInt128 GetNthOrderSuperSequenceLength(string sequence, int intermediateRobotCount);

        protected Dictionary<string, UInt128> CountSuperSequenceDeltaSequences(string sequence)
        {
            var counts = new Dictionary<string, UInt128>();
            char fromKey = 'A';

            foreach (char toKey in sequence)
            {
                counts.Merge(Sequences[(fromKey, toKey)], UInt128.One, UInt128.Zero, Add);
                fromKey = toKey;
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

        private static UInt128 Add(string key, UInt128 oldValue, UInt128 newValue) => oldValue + newValue;

        private static StringBuilder AppendHorizontal(int dx, StringBuilder stringBuilder)
        {
            return stringBuilder.Append((dx < 0) ? '<' : '>', Math.Abs(dx));
        }

        private static StringBuilder AppendVertical(int dy, StringBuilder stringBuilder)
        {
            return stringBuilder.Append((dy < 0) ? '^' : 'v', Math.Abs(dy));
        }

    }

}