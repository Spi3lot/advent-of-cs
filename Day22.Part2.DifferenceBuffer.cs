namespace AdventOfCode;

public partial record Day22
{

    private sealed class DifferenceBuffer(int length)
    {

        public int[] Differences { get; } = new int[length];

        public int Base19Representation { get; private set; }

        public static DifferenceBuffer FromBase19(int base19, int length)
        {
            var buffer = new DifferenceBuffer(length) { Base19Representation = base19 };
            var division = Math.DivRem(base19, 1);

            for (int i = 0; i < length; i++)
            {
                division = Math.DivRem(division.Quotient, 19);
                buffer.Differences[i] = division.Remainder - 9;
            }

            return buffer;
        }

        public void Shift(int newDifference)
        {
            for (int i = 0; i < Differences.Length - 1; i++)
            {
                Differences[i] = Differences[i + 1];
            }

            Differences[^1] = newDifference;
            Base19Representation = ToBase19();
        }

        private int ToBase19()
        {
            int result = 0;
            int factor = 1;

            foreach (int difference in Differences)
            {
                result += (difference + 9) * factor;
                factor *= 19;
            }

            return result;
        }

    }

}