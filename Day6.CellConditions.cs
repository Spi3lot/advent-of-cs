namespace AdventOfCode;

public partial record Day6
{

    private static CellConditions ParseGridFlags(char gridElement)
    {
        return gridElement switch
        {
            '>' => CellConditions.Visited | CellConditions.FacedRight,
            '^' => CellConditions.Visited | CellConditions.FacedUp,
            '<' => CellConditions.Visited | CellConditions.FacedLeft,
            'v' => CellConditions.Visited | CellConditions.FacedDown,
            '#' => CellConditions.Obstacle,
            '.' => CellConditions.None,
            _ => throw new InvalidOperationException($"No pattern matched '{gridElement}'")
        };
    }

    private static CellConditions GetFlagFromDirection((int x, int y) direction)
    {
        return direction switch
        {
            (1, 0) => CellConditions.FacedRight,
            (0, -1) => CellConditions.FacedUp,
            (-1, 0) => CellConditions.FacedLeft,
            (0, 1) => CellConditions.FacedDown,
            _ => throw new ArgumentException($"Invalid value for {nameof(direction)}: {direction}")
        };
    }

    private static (int x, int y) GetDirectionFromFlag(CellConditions flags)
    {
        if (flags.HasFlag(CellConditions.FacedRight)) return (1, 0);
        if (flags.HasFlag(CellConditions.FacedUp)) return (0, -1);
        if (flags.HasFlag(CellConditions.FacedLeft)) return (-1, 0);
        if (flags.HasFlag(CellConditions.FacedDown)) return (0, 1);
        throw new ArgumentException($"Invalid flags: {flags}");
    }

    [Flags]
    private enum CellConditions : byte
    {

        None = 0,
        FacedRight = 1,
        FacedUp = 2,
        FacedLeft = 4,
        FacedDown = 8,
        Visited = 16,
        Obstacle = 32

    }

}