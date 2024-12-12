using System.Collections.Frozen;

namespace AdventOfCode;

public partial record Day10
{

    private sealed class Vertex(int height)
    {

        public static readonly (int dx, int dy)[] NeighborDirections =
        [
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1)
        ];

        public int Height { get; } = height;

        public ICollection<Vertex> Neighbors { get; } = [];

    }

}