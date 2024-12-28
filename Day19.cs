namespace AdventOfCode;

public record Day19 : AdventDay<Day19>
{

    private readonly string[] _availableTowels;

    private readonly string[] _designs;

    private readonly Dictionary<string, long> _arrangementCounts = [];

    public Day19()
    {
        string[] parts = Input.Split("\n\n");
        _availableTowels = parts[0].Split(", ");
        _designs = parts[1].TrimEnd().Split('\n');
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_designs.Count(design => CountPossibleArrangements(design) > 0));
    }

    public override void SolvePart2()
    {
        Console.WriteLine(_designs.Sum(CountPossibleArrangements));
    }

    private long CountPossibleArrangements(string design)
    {
        if (design.Length == 0) return 1;
        if (_arrangementCounts.TryGetValue(design, out long count)) return count;

        count = _availableTowels
            .Where(design.StartsWith)
            .Select(towel => design[towel.Length..])
            .Sum(CountPossibleArrangements);

        _arrangementCounts[design] = count;
        return count;
    }

}