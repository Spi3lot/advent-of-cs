namespace AdventOfCode;

public partial record Day11
{

    private sealed class Stone(ulong number)
    {

        public ulong Number { get; private set; } = number;

        public Stone? ApplyRule()
        {
            if (Number == 0)
            {
                Number = 1;
            }
            else if (CountDigits(Number) % 2 == 0)
            {
                (Number, ulong rightHalf) = SplitNumber(Number);
                return new Stone(rightHalf);
            }
            else
            {
                Number *= 2024;
            }

            return null;
        }

        public ulong CountDescendants(int blinkCount, IDictionary<(ulong StoneNumber, int BlinkCount), ulong> cache)
        {
            if (blinkCount == 0) return 1;
            if (cache.ContainsKey((Number, blinkCount))) return cache[(Number, blinkCount)];
            var leftStone = new Stone(Number);
            var rightStone = leftStone.ApplyRule();
            ulong descendantCount = leftStone.CountDescendants(blinkCount - 1, cache);
            if (rightStone != null) descendantCount += rightStone.CountDescendants(blinkCount - 1, cache);
            cache.Add((Number, blinkCount), descendantCount);
            return descendantCount;
        }

    }

    private static int CountDigits(ulong number) => 1 + (int) Math.Log10(number);

    private static (ulong, ulong) SplitNumber(ulong number)
    {
        int digitCount = CountDigits(number);
        ulong divisor = Day7.Equation.Pow(10, digitCount / 2);
        return Math.DivRem(number, divisor);
    }

}