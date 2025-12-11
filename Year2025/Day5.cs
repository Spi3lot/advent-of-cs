namespace AdventOfCode.Year2025;

public partial record Day5() : AdventDay<Day5>(2025)
{

    public override void SolvePart1()
    {
        string[][] ids = Input.Trim()
            .Split("\n\n")
            .Select(ids => ids.Split('\n'))
            .ToArray();

        var ranges = ids[0]
            .Select(Range.FromString)
            .ToArray();

        int count = ids[1]
            .Select(long.Parse)
            .Count(id => ranges.Any(range => range.Start <= id && id <= range.End));

        Console.WriteLine(count);
    }


    public override void SolvePart2()
    {
        string[][] ids = Input.Trim()
            .Split("\n\n")
            .Select(ids => ids.Split('\n'))
            .ToArray();

        var ranges = ids[0]
            .Select(Range.FromString)
            .OrderBy(range => range.Start)
            .ToList();

        int i = 0;
        
        while (i < ranges.Count - 1)
        {
            if (ranges[i].Merge(ranges[i + 1]))
            {
                ranges.RemoveAt(i + 1);
            }
            else
            {
                i++;
            }
        }

        Console.WriteLine(ranges.Sum(range => range.Count));
    }

}