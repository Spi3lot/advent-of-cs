namespace AdventOfCode;

public partial record Day11 : AdventDay<Day11>
{

    private sealed class Stone(ulong number)
    {

        public Stone? ApplyRule()
        {
            if (number == 0) number = 1;
            else if (CountDigits(number) % 2 == 0) { (number, ulong RightHalf) = SplitNumber(number); return new Stone(RightHalf); }
            else number *= 2024;
            return null;
        }

    }

    public static int CountDigits(ulong number) => 1 + (int)Math.Log10(number);

    public static (ulong, ulong) SplitNumber(ulong number)
    {
        int digitCount = CountDigits(number);
        ulong divisor = Day7.Equation.Pow(10, digitCount / 2);
        return Math.DivRem(number, divisor);
    }

}