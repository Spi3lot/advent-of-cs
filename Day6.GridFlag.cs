namespace AdventOfCode;

public partial record Day6
{

    private static GridElements ParseGridFlags(char gridElement)
    {
        return gridElement switch
        {
            '>' => GridElements.Visited | GridElements.FacedRight,
            '^' => GridElements.Visited | GridElements.FacedUp,
            '<' => GridElements.Visited | GridElements.FacedLeft,
            'v' => GridElements.Visited | GridElements.FacedDown,
            '#' => GridElements.Obstacle,
            '.' => GridElements.None,
            _ => throw new InvalidOperationException($"No pattern matched '{gridElement}'")
        };
    }

    private static GridElements GetFlagFromDirection((int x, int y) direction)
    {
        return direction switch
        {
            (1, 0) => GridElements.FacedRight,
            (0, -1) => GridElements.FacedUp,
            (-1, 0) => GridElements.FacedLeft,
            (0, 1) => GridElements.FacedDown,
            _ => throw new ArgumentException($"Invalid value for {nameof(direction)}: {direction}")
        };
    }

    private static (int x, int y) GetDirectionFromFlag(GridElements flags)
    {
        if (flags.HasFlag(GridElements.FacedRight)) return (1, 0);
        if (flags.HasFlag(GridElements.FacedUp)) return (0, -1);
        if (flags.HasFlag(GridElements.FacedLeft)) return (-1, 0);
        if (flags.HasFlag(GridElements.FacedDown)) return (0, 1);
        throw new ArgumentException($"Invalid flags: {flags}");
    }

    [Flags]
    private enum GridElements : byte
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