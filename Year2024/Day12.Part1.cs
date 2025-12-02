namespace AdventOfCode.Year2024;

using Position = (int X, int Y);

public partial record Day12
{

    public override void SolvePart1()
    {
        Console.WriteLine(CalcTotalFencingPrice(CalcRegionPerimeter));
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