using System.Text.RegularExpressions;

namespace AdventOfCode;

public record Day3(string Input) : AdventDay(Input)
{

    public override void SolvePart1()
    {
        ulong sum = 0;

        foreach (Match match in Regex.Matches(Input, @"(?:mul\((\d{1,3}),(\d{1,3})\))"))
        {
            var factor1 = ulong.Parse(match.Groups[1].Value);
            var factor2 = ulong.Parse(match.Groups[2].Value);
            sum += factor1 * factor2;
        }

        Console.WriteLine(sum);
    }

    public override void SolvePart2()
    {
        bool multiply = true;
        ulong sum = 0;

        foreach (Match match in Regex.Matches(Input, @"do(?:n't)?\(\)|mul\(\d{1,3},\d{1,3}\)"))
        {
            string value = match.Value;

            if (value.StartsWith("mul"))
            {
                if (multiply)
                {
                    var numbers = Regex.Match(value, @"(\d+),(\d+)");
                    var factor1 = ulong.Parse(numbers.Groups[1].Value);
                    var factor2 = ulong.Parse(numbers.Groups[2].Value);
                    sum += factor1 * factor2;
                }
            }
            else
            {
                multiply = value switch
                {
                    "do()" => true,
                    "don't()" => false,
                    _ => throw new InvalidOperationException(value),
                };
            }
        }

        Console.WriteLine(sum);
    }

}