namespace AdventOfCode;

public partial record Day6
{

    [Flags]
    private enum GridFlag : byte
    {
        Empty = 0,
        FacedRight = 1,
        FacedUp = 2,
        FacedLeft = 4,
        FacedDown = 8,
        Visited = 16,
        Obstacle = 32,
    }

    private static GridFlag ParseGridFlags(char gridElement)
    {
        return gridElement switch
        {
            '>' => GridFlag.Visited | GridFlag.FacedRight,
            '^' => GridFlag.Visited | GridFlag.FacedUp,
            '<' => GridFlag.Visited | GridFlag.FacedLeft,
            'v' => GridFlag.Visited | GridFlag.FacedDown,
            '#' => GridFlag.Obstacle,
            '.' => GridFlag.Empty,
            _ => throw new InvalidOperationException($"No pattern matched '{gridElement}'")
        };
    }

    private static GridFlag GetFlagFromDirection((int x, int y) direction)
    {
        return direction switch
        {
            (1, 0) => GridFlag.FacedRight,
            (0, -1) => GridFlag.FacedUp,
            (-1, 0) => GridFlag.FacedLeft,
            (0, 1) => GridFlag.FacedDown,
            _ => throw new ArgumentException($"Invalid value for {nameof(direction)}: {direction}")
        };
    }

    private static (int x, int y) GetDirectionFromFlag(GridFlag flags)
    {
        if (flags.HasFlag(GridFlag.FacedRight)) return (1, 0);
        if (flags.HasFlag(GridFlag.FacedUp)) return (0, -1);
        if (flags.HasFlag(GridFlag.FacedLeft)) return (-1, 0);
        if (flags.HasFlag(GridFlag.FacedDown)) return (0, 1);
        throw new ArgumentException($"Invalid flags: {flags}");
    }

}