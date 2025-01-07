namespace AdventOfCode;

public partial record Day22
{

    private sealed class DifferenceBuffer(int length)
    {

        private readonly int _maxFactor = Convert.ToInt32(Math.Pow(19, length - 1));

        private readonly int[] _differences = new int[length];

        private int _startIndex = 0;

        public int Base19Representation { get; private set; }

        public int this[int index] => _differences[ShiftedIndex(index)];

        public static DifferenceBuffer FromBase19(int base19, int length)
        {
            var buffer = new DifferenceBuffer(length) { Base19Representation = base19 };
            var division = Math.DivRem(base19, 1);

            for (int i = 0; i < length; i++)
            {
                division = Math.DivRem(division.Quotient, 19);
                buffer._differences[i] = division.Remainder - 9;
            }

            return buffer;
        }

        public void Shift(int newDifference)
        {
            _differences[_startIndex] = newDifference;
            _startIndex = ShiftedIndex(1);
            Base19Representation = Base19Representation / 19 + newDifference * _maxFactor;
        }

        private int ShiftedIndex(int index) => (_startIndex + index) % _differences.Length;

    }

}