namespace AdventOfCode;

public record Day5 : AdventDay<Day5>
{

    public override void SolvePart1()
    {
        string[] split = Input.Split("\n\n");

        Page.Rules = split[0].Split('\n')
            .Select(ruleString => ruleString.Split('|'))
            .ToLookup(rule => int.Parse(rule[0]), rule => int.Parse(rule[1]));

        int sum = split[1].Split('\n')
            .Where(updateString => !string.IsNullOrWhiteSpace(updateString))
            .Select(updateString => updateString.Split(','))
            .Select(update => update.Select(pageString => new Page(int.Parse(pageString))).ToList())
            .Where(update =>
            {
                var sortedUpdate = new List<Page>(update);
                sortedUpdate.Sort();
                return sortedUpdate.SequenceEqual(update);
            })
            .Select(update => update[update.Count / 2])
            .Sum(middlePage => middlePage.Number);

        Console.WriteLine(sum);
    }

    public override void SolvePart2()
    {
        string[]? split = Input.Split("\n\n");

        Page.Rules = split[0].Split('\n')
            .Select(ruleString => ruleString.Split('|'))
            .ToLookup(rule => int.Parse(rule[0]), rule => int.Parse(rule[1]));

        int sum = split[1].Split('\n')
            .Where(updateString => !string.IsNullOrWhiteSpace(updateString))
            .Select(updateString => updateString.Split(','))
            .Select(update => update.Select(pageString => new Page(int.Parse(pageString))).ToList())
            .Select(update =>
            {
                var sortedUpdate = new List<Page>(update);
                sortedUpdate.Sort();
                return (unsorted: update, sorted: sortedUpdate);
            })
            .Where((updates) => !updates.sorted.SequenceEqual(updates.unsorted))
            .Select((updates) => updates.sorted)
            .Select(update => update[update.Count / 2])
            .Sum(middlePage => middlePage.Number);

        Console.WriteLine(sum);
    }

    private sealed record Page(int Number) : IComparable<Page>
    {

        // Each key precedes all the corresponding values
        public static ILookup<int, int>? Rules; // ILookup = "multi-valued Dictionary"

        public int CompareTo(Page? other)
        {
            if (other == this) return 0;
            if (other == null) throw new InvalidOperationException($"{nameof(other)} shall not be null");
            if (Rules == null) throw new InvalidOperationException($"{nameof(Rules)} shall not be null");
            if (Rules.Contains(Number) && Rules[Number].Contains(other.Number)) return -1;  // this is a predecessor of other, other is a successor of this
            if (Rules.Contains(other.Number) && Rules[other.Number].Contains(Number)) return 1; // this is a successor of other, other is a predecessor of this
            return 0;
        }

    }

}