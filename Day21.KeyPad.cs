using System.Text;

namespace AdventOfCode;

public partial record Day21
{

    private sealed class KeyPad
    {

        private readonly Dictionary<char, (int X, int Y)> _positions = [];

        private readonly (int X, int Y) _gapPosition;

        private readonly string[] _layout;

        public KeyPad(params string[] layout)
        {
            foreach (var (j, line) in layout.Index())
            {
                foreach (var (i, key) in line.Index())
                {
                    _positions[key] = (i, j);
                }
            }

            _gapPosition = _positions[' '];
            _layout = layout;
        }

        public string Press(string sequence)
        {
            var stringBuilder = new StringBuilder(sequence.Count(key => key == 'A'));
            var position = _positions['A'];

            foreach (char key in sequence)
            {
                switch (key)
                {
                    case 'A':
                        stringBuilder.Append(_layout[position.Y][position.X]);
                        break;

                    case '<':
                        position.X--;
                        break;

                    case '>':
                        position.X++;
                        break;

                    case '^':
                        position.Y--;
                        break;

                    case 'v':
                        position.Y++;
                        break;

                    default:
                        throw new ArgumentException($"Sequence contains an invalid character: {key}");
                }
            }

            return stringBuilder.ToString();
        }

        public string GetSequenceForPressing(string sequence)
        {
            var stringBuilder = new StringBuilder();
            var previousPosition = _positions['A'];

            foreach (var position in sequence.Select(key => _positions[key]))
            {
                stringBuilder.Append(GetSequenceForDifference(previousPosition, position));
                previousPosition = position;
            }

            return stringBuilder.ToString();
        }

        private string GetSequenceForDifference((int X, int Y) from, (int X, int Y) to)
        {
            (int X, int Y) delta = (to.X - from.X, to.Y - from.Y);
            var stringBuilder = new StringBuilder(Math.Abs(delta.X) + Math.Abs(delta.Y) + 1);

            if (from.X == _gapPosition.X)
            {
                AppendHorizontal(delta.X, stringBuilder);
                AppendVertical(delta.Y, stringBuilder);
            }
            else if (from.Y == _gapPosition.Y)
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
                    (< 0, < 0) => stringBuilder.Append('^', -delta.Y).Append('<', -delta.X), // ^ is closer to A than <
                    (< 0, > 0) => stringBuilder.Append('v', delta.Y).Append('<', -delta.X), // v is closer to A than <
                    (> 0, < 0) => stringBuilder.Append('>', delta.X).Append('^', -delta.Y), // > is as close to A as ^
                    (> 0, > 0) => stringBuilder.Append('>', delta.X).Append('v', delta.Y), // > is closer to A than v
                };
            }

            return stringBuilder.Append('A').ToString();
        }

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