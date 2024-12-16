namespace AdventOfCode;

public record Day12 : AdventDay<Day12>
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
        Console.WriteLine(CalcAreaSideCountFencingPriceForRegion((0, 1), new bool[_farm.Length, _farm[0].Length]));
        // Console.WriteLine(CalcTotalFencingPrice(CalcAreaSideCountFencingPriceForRegion));
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

    private int CalcAreaPerimeterFencingPriceForRegion((int X, int Y) position, bool[,] covered)
    {
        HashSet<(int X, int Y)> region = [];
        int perimeter = GetRegionPerimeter(_farm[position.Y][position.X], position, region);
        int area = region.Count;
        foreach ((int x, int y) in region) covered[y, x] = true;
        return area * perimeter;
    }

    private int GetRegionPerimeter(
        char plantType,
        (int X, int Y) pos,
        HashSet<(int, int)> region
    )
    {
        if (IsOnPerimeter(pos, plantType)) return 1;
        if (region.Contains(pos)) return 0;
        region.Add((pos.X, pos.Y));
        return Deltas.Sum(delta => GetRegionPerimeter(plantType, (pos.X + delta.X, pos.Y + delta.Y), region));
    }

    private int CalcAreaSideCountFencingPriceForRegion((int X, int Y) position, bool[,] covered)
    {
        HashSet<(int X, int Y)> region = [];

        int perimeter = GetRegionSideCount(
            _farm[position.Y][position.X],
            region,
            position,
            null,
            new bool[4]
        );

        foreach ((int x, int y) in region)
        {
            covered[y, x] = true;
        }

        int area = region.Count;
        return area * perimeter;
    }

    private int GetRegionSideCount(
        char plantType,
        HashSet<(int, int)> region,
        (int X, int Y) pos,
        int? deltaIndex,
        bool[] alreadyCountedPerimeterSide
    )
    {
        if (IsOnPerimeter(pos, plantType))
            return (deltaIndex == null || !alreadyCountedPerimeterSide[deltaIndex.Value]) ? 1 : 0;

        if (region.Contains(pos)) return 0;
        region.Add((pos.X, pos.Y));

        for (int i = 0; i < Deltas.Length; i++)
        {
            alreadyCountedPerimeterSide[i] |= IsOnPerimeter(
                (pos.X + Deltas[i].X, pos.Y + Deltas[i].Y),
                plantType
            );
        }

        return Deltas
            .Select((delta, index) => GetRegionSideCount(
                plantType,
                region,
                (pos.X + delta.X, pos.Y + delta.X),
                index,
                CopyAndSetIndexToFalse(alreadyCountedPerimeterSide, index) // Should not be necessary to set to false
            ))
            .Sum();
    }

    private bool IsOnPerimeter((int X, int Y) pos, char plantType)
    {
        return !Day4.IsOnGrid(pos, _farm) || _farm[pos.Y][pos.X] != plantType;
    }

    private static bool[] CopyAndSetIndexToFalse(bool[] array, int index)
    {
        return array
            .Select((item, i) => item && (i != index))
            .ToArray();
    }

}