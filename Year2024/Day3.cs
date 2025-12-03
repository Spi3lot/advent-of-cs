using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

public record Day3 : AdventDay<Day3>
{

    public override void SolvePart1()
    {
        ulong sum = 0;

        foreach (var group in Regex.Matches(Input, @"(?:mul\((\d{1,3}),(\d{1,3})\))").Select(match => match.Groups))
        {
            ulong factor1 = ulong.Parse(group[1].Value);
            ulong factor2 = ulong.Parse(group[2].Value);
            sum += factor1 * factor2;
        }

        Console.WriteLine(sum);
    }

    public override void SolvePart2()
    {
        bool multiply = true;
        ulong sum = 0;

        foreach (string value in Regex.Matches(Input, @"do(?:n't)?\(\)|mul\(\d{1,3},\d{1,3}\)")
                     .Select(match => match.Value))
        {
            if (value.StartsWith("mul"))
            {
                if (!multiply) continue;

                var numbers = Regex.Match(value, @"(\d+),(\d+)");
                ulong factor1 = ulong.Parse(numbers.Groups[1].Value);
                ulong factor2 = ulong.Parse(numbers.Groups[2].Value);
                sum += factor1 * factor2;
            }
            else
            {
                multiply = value switch
                {
                    "do()" => true,
                    "don't()" => false,
                    _ => throw new InvalidOperationException(value)
                };
            }
        }

        Console.WriteLine(sum);
    }

}