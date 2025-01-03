using System.ComponentModel.DataAnnotations;

namespace AdventOfCode;

public partial record Day22
{

    private sealed class DifferenceBuffer(int length)
    {

        private readonly int[] _differences = new int[length];

        public int Length => length;

        public int Base19Representation { get; private set; }

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
            for (int i = 0; i < _differences.Length - 1; i++)
            {
                _differences[i] = _differences[i + 1];
            }

            _differences[^1] = newDifference;
            Base19Representation = ToBase19();
        }

        private int ToBase19()
        {
            int result = 0;
            int factor = 1;

            foreach (int n in from d in _differences select d + 9)
            {
                result += (n + 9) * factor;
                factor *= 19;
            }

            return result;
        }

        public bool IsPossible()
        {
            return Enumerable.Range(0, 10).Any(IsPossibleWithInitialBananaAmount);
        }

        public bool IsPossibleWithInitialBananaAmount([Range(0, 10)] int bananaAmount)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(bananaAmount, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bananaAmount, 10);

            foreach (int difference in _differences)
            {
                bananaAmount += difference;
                if (bananaAmount < 0 || bananaAmount >= 10) return false;
            }

            return true;
        }

    }

}