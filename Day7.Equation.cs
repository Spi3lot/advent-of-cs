using System.ComponentModel.DataAnnotations;

namespace AdventOfCode;

public partial record Day7
{

    private record Equation(params ulong[] Numbers)
    {

        public readonly int OperatorGapCount = Numbers.Length - 2;

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
            ulong variationCount = IPow(operatorCount, OperatorGapCount);

            for (ulong trits = 0; trits < variationCount; trits++)
            {
                if (EvaluateRightHandSide(operatorCount, trits) == expectedResult)
                {
                    return true;
                }
            }

            return false;
        }

        public ulong EvaluateRightHandSide([Range(1, 3)] int operatorCount, ulong operatorTrits)
        {
            ulong result = Numbers[1];

            for (int i = 0; i < OperatorGapCount; i++)
            {
                ulong number = Numbers[i + 2];
                int @operator = ((int)(operatorTrits / IPow(operatorCount, i))) % operatorCount;  // Cast to int cuts off the upper 32 bits, just like necessary here

                result = @operator switch
                {
                    0 => result + number,
                    1 => result * number,
                    2 => result * IPow(10, 1 + (int)Math.Log10(number)) + number,
                    _ => throw new Exception("This is not even possible")
                };

            }

            return result;
        }

        private static ulong IPow(int @base, int exponent)
        {
            return Convert.ToUInt64(Math.Pow(@base, exponent));
        }

    }


}