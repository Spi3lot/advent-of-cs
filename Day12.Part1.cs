namespace AdventOfCode;

using Position = (int X, int Y);

public partial record Day12
{

    private int CalcAreaPerimeterFencingPriceForRegion(Position position, bool[,] covered)
    {
        HashSet<Position> region = [];
        int perimeter = CalcRegionPerimeter(_farm[position.Y][position.X], position, region);

        foreach ((int x, int y) in region)
        {
            covered[y, x] = true;
        }

        int area = region.Count;
        return area * perimeter;
    }

    private int CalcRegionPerimeter(
        char plantType,
        Position pos,
        HashSet<Position> region
    )
    {
        if (IsOnPerimeter(pos, plantType)) return 1;
        if (region.Contains(pos)) return 0;
        region.Add((pos.X, pos.Y));
        return Deltas.Sum(delta => CalcRegionPerimeter(plantType, (pos.X + delta.X, pos.Y + delta.Y), region));
    }

}