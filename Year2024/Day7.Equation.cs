using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AdventOfCode.Year2024;

public partial record Day7
{

    public sealed record Equation(params ulong[] Numbers)
    {

        private readonly int _operatorGapCount = Numbers.Length - 2;

        public static Equation Parse(string line)
        {
            return new Equation(
                line.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries)
                    .Select(ulong.Parse)
                    .ToArray()
            );
        }

        public bool IsSolvable([Range(1, 3)] int operatorCount)
        {
            ulong expectedResult = Numbers[0];
            ulong variationCount = Pow(operatorCount, _operatorGapCount);

            for (ulong trits = 0; trits < variationCount; trits++)
            {
                if (EvaluateRightHandSide(operatorCount, trits) == expectedResult)
                {
                    return true;
                }
            }

            return false;
        }

        private ulong EvaluateRightHandSide([Range(1, 3)] int operatorCount, ulong operatorTrits)
        {
            ulong result = Numbers[1];

            for (int i = 0; i < _operatorGapCount; i++)
            {
                ulong number = Numbers[i + 2];

                int @operator = (int)(operatorTrits / Pow(operatorCount, i)) %
                                operatorCount; // Cast to int cuts off the upper 32 bits, just like necessary here

                result = @operator switch
                {
                    0 => result + number,
                    1 => result * number,
                    2 => result * Pow(10, 1 + (int)Math.Log10(number)) + number,
                    _ => throw new ConstraintException(
                        $"{nameof(operatorCount)} can only be 1, 2 or 3, not {operatorCount}"
                    )
                };
            }

            return result;
        }

        public static ulong Pow(int @base, int exponent)
        {
            return Convert.ToUInt64(Math.Pow(@base, exponent));
        }

    }

}