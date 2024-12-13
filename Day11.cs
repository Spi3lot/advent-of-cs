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

    public override void SolvePart1()
    {
        // Iterative
        for (int i = 0; i < 25; i++)
        {
            foreach (var stone in (ICollection<Stone>)[.. _stones])
            {
                var halfStone = stone.ApplyRule();
                if (halfStone != null) _stones.Add(halfStone);
            }
        }

        Console.WriteLine(_stones.Count);

        // Recursive
        ulong descendantCount = 0;

        foreach (var item in _stones)
        {
            descendantCount += item.CountDescendants(25);
        }

        Console.WriteLine(descendantCount);
    }

    public override void SolvePart2()
    {

    }

}