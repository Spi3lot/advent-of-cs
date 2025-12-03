namespace AdventOfCode.Year2024;

using Position = (int X, int Y);

public partial record Day12 : AdventDay<Day12>
{

    private static readonly Position[] Deltas = [(1, 0), (0, 1), (-1, 0), (0, -1)];

    private readonly string[] _farm;

    public Day12()
    {
        _farm = Input.Split("\n")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();
    }

    private int CalcTotalFencingPrice(Func<char, Position, HashSet<Position>, int> calcFactor)
    {
        int totalFencingPrice = 0;
        bool[,] covered = new bool[_farm.Length, _farm[0].Length];

        for (int j = 0; j < covered.GetLength(0); j++)
        {
            for (int i = 0; i < covered.GetLength(1); i++)
            {
                if (!covered[j, i])
                {
                    totalFencingPrice += CalcFencingPriceForRegion((i, j), covered, calcFactor);
                }
            }
        }

        return totalFencingPrice;
    }

    private int CalcFencingPriceForRegion(
        Position position,
        bool[,] covered,
        Func<char, Position, HashSet<Position>, int> calcFactor
    )
    {
        HashSet<Position> region = [];
        int factor = calcFactor(_farm[position.Y][position.X], position, region);

        foreach ((int x, int y) in region)
        {
            covered[y, x] = true;
        }

        int area = region.Count;
        return area * factor;
    }

    private bool IsOnPerimeter(Position pos, char plantType)
    {
        return !Day4.IsOnGrid(pos, _farm) || _farm[pos.Y][pos.X] != plantType;
    }

}