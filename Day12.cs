namespace AdventOfCode;

public partial record Day12 : AdventDay<Day12>
{

    private static readonly (int X, int Y)[] Deltas = [(1, 0), (0, 1), (-1, 0), (0, -1)];

    private readonly string[] _farm;

    public Day12()
    {
        _farm = Input.Split("\n")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();
    }

    public override void SolvePart1()
    {
        Console.WriteLine(CalcTotalFencingPrice(CalcAreaPerimeterFencingPriceForRegion));
    }

    public override void SolvePart2()
    {
        Console.WriteLine(CalcTotalFencingPrice(CalcAreaSideCountFencingPriceForRegion));
    }

    private int CalcTotalFencingPrice(Func<(int X, int Y), bool[,], int> calcFencingPrice)
    {
        int totalFencingPrice = 0;
        bool[,] covered = new bool[_farm.Length, _farm[0].Length];

        for (int j = 0; j < covered.GetLength(0); j++)
        {
            for (int i = 0; i < covered.GetLength(1); i++)
            {
                if (!covered[j, i])
                {
                    totalFencingPrice += calcFencingPrice((i, j), covered);
                }
            }
        }

        return totalFencingPrice;
    }

    private bool IsOnPerimeter((int X, int Y) pos, char plantType)
    {
        return !Day4.IsOnGrid(pos, _farm) || _farm[pos.Y][pos.X] != plantType;
    }

}