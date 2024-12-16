namespace AdventOfCode;

public partial record Day12
{

    private int CalcAreaPerimeterFencingPriceForRegion((int X, int Y) position, bool[,] covered)
    {
        HashSet<(int X, int Y)> region = [];
        int perimeter = GetRegionPerimeter(_farm[position.Y][position.X], position, region);
        int area = region.Count;

        foreach ((int x, int y) in region)
        {
            covered[y, x] = true;
        }

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

}