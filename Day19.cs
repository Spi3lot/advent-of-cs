namespace AdventOfCode;

public record Day19 : AdventDay<Day19>
{

    private readonly string[] _availableTowels;

    private readonly string[] _designs;


    public Day19()
    {
        string[] parts = Input.Split("\n\n");
        _availableTowels = parts[0].Split(", ").OrderByDescending(towel => towel.Length).ToArray();
        Console.WriteLine(string.Join(", ", _availableTowels));
        _designs = parts[1].TrimEnd().Split('\n');
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_designs.Count(design =>
        {
            bool possible = IsDesignPossibleBroken2(design);
            Console.WriteLine(possible);
            return possible;
        }));
    }

    public override void SolvePart2() { }

    private bool IsDesignPossibleBroken2(string design)
    {
        foreach (string candidate in _availableTowels)
        {
            int index;

            while ((index = design.IndexOf(candidate, StringComparison.Ordinal)) >= 0)
            {
                design = design.Remove(index, candidate.Length);
            }
        }

        return design.Length == 0;
    }

    private bool IsDesignPossibleBroken1(string design)
    {
        var candidates = _availableTowels.ToHashSet();

        while (candidates.Count > 0)
        {
            foreach (string candidate in candidates.ToArray())
            {
                if (design.Length == 0) return true;
                int index = design.IndexOf(candidate, StringComparison.Ordinal);
                if (index < 0) candidates.Remove(candidate);
                else design = design.Remove(index, candidate.Length);
            }
        }

        return false;
    }

    // Works theoretically but way too slow
    private bool IsDesignPossible(string desiredDesign, string currentDesign = "")
    {
        if (desiredDesign == currentDesign) return true;
        string remainingDesign = desiredDesign[currentDesign.Length..];

        for (int length = 1; length <= remainingDesign.Length; length++)
        {
            string slice = remainingDesign[..length];
            int index = Array.BinarySearch(_availableTowels, slice);

            if (index >= 0 && IsDesignPossible(desiredDesign, currentDesign + slice))
            {
                return true;
            }
        }

        return false;
    }

}