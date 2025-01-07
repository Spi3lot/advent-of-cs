namespace AdventOfCode;

public partial record Day22
{

    private sealed class DifferenceBuffer(int length)
    {

        private readonly sbyte[] _differences = new sbyte[length];

        private readonly int _maxDigitFactor = Convert.ToInt32(Math.Pow(19, length - 1));

        public readonly int CombinationCount = Convert.ToInt32(Math.Pow(19, length));

        private int _startIndex;

        public int Base19 { get; private set; }

        public int this[int index] => _differences[ShiftedIndex(index)];

        public static DifferenceBuffer FromBase19(int base19, int length)
        {
            var buffer = new DifferenceBuffer(length) { Base19 = base19 };
            var division = (Quotient: base19, Remainder: 0);

            for (int i = 0; i < length; i++)
            {
                division = Math.DivRem(division.Quotient, 19);
                buffer._differences[i] = (sbyte) (division.Remainder - 9);
            }

            return buffer;
        }

        public void Shift(int newDifference)
        {
            _differences[_startIndex] = (sbyte) newDifference;
            _startIndex = ShiftedIndex(1);
            Base19 = Base19 / 19 + (newDifference + 9) * _maxDigitFactor;
        }

        private int ShiftedIndex(int index) => (_startIndex + index) % _differences.Length;

    }

}