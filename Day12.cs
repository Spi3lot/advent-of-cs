using System.Collections.ObjectModel;

namespace AdventOfCode;

public partial record Day12 : AdventDay<Day12>
{

    private string[] _farm;

    public Day12()
    {
        _farm = Input.Split("\n")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();
    }

    public override void SolvePart1()
    {
        int totalFencingPrice = 0;

        for (int j = 0; j < _farm.Length; j++)
        {
            for (int i = 0; i < _farm[j].Length; i++)
            {
                var region = GetRegion((i, j));
                totalFencingPrice += CalcFencingPriceForRegion(region);
            }
        }

        Console.WriteLine(totalFencingPrice);
    }

    private static int CalcFencingPriceForRegion(HashSet<(int X, int Y)> region)
    {
        int area = region.Count;
        int perimeter = 0; // TODO
        return area * perimeter;
    }

    public override void SolvePart2() { }

    private HashSet<(int X, int Y)> GetRegion((int X, int Y) position)
    {
        HashSet<(int X, int Y)> region = [];
        GetRegion(_farm[position.Y][position.X], position, region);
        return region;
    }

    private void GetRegion(
        char plantType,
        (int X, int Y) position,
        HashSet<(int, int)> region
    )
    {
        if (!Day4.IsOnGrid(position, _farm) ||
            _farm[position.Y][position.X] != plantType ||
            region.Contains(position))
        {
            return;
        }

        region.Add((position.X, position.Y));
        GetRegion(plantType, (position.X - 1, position.Y), region);
        GetRegion(plantType, (position.X + 1, position.Y), region);
        GetRegion(plantType, (position.X, position.Y - 1), region);
        GetRegion(plantType, (position.X, position.Y + 1), region);
    }

}