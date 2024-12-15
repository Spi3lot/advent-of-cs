namespace AdventOfCode;

public partial record Day11 : AdventDay<Day11>
{

    private readonly ICollection<Stone> _stones;

    public Day11()
    {
        _stones = Input.Split(' ')
            .Select(str => new Stone(ulong.Parse(str)))
            .ToList();
    }

    // Iterative
    public override void SolvePart1()
    {
        var stones = _stones.Select(stone => new Stone(stone.Number)).ToList();

        for (int i = 0; i < 25; i++)
        {
            foreach (var stone in (ICollection<Stone>) [.. stones])
            {
                var halfStone = stone.ApplyRule();
                if (halfStone != null) stones.Add(halfStone);
            }
        }

        Console.WriteLine(stones.Count);
    }

    // Recursive
    public override void SolvePart2()
    {
        var cache = new Dictionary<(ulong, int), ulong>();

        checked
        {
            ulong total = _stones.Aggregate<Stone, ulong>(0,
                (current, stone) => current + stone.CountDescendants(75, cache));

            Console.WriteLine(total);
        }
    }

}