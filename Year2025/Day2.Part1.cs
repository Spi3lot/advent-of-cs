namespace AdventOfCode.Year2025;

public partial record Day2
{

    public override void SolvePart1()
    {
        long sum = Input.Split(',')
            .Select(range => range.Split('-'))
            .Select(range => range.Select(long.Parse).ToArray())
            .Select(range => LongRange(range[0], range[1]))
            .Sum(range => range.Where(IsIdDuplicate).Sum());

        Console.WriteLine(sum);
    }

    private static bool IsIdDuplicate(long id)
    {
        string s = id.ToString();
        int mid = s.Length / 2;
        return s.Length % 2 == 0 && s[..mid] == s[mid..];
    }

}