namespace AdventOfCode;

using Position = (int X, int Y);
using Plot = ((int X, int Y) Position, bool[] CountPerimeterSide);

public partial record Day12
{

    public override void SolvePart2()
    {
        Console.WriteLine(CalcTotalFencingPrice(CalcRegionSideCount));
    }

    private int CalcRegionSideCount(
        char plantType,
        Position initialPosition,
        HashSet<(int, int)> region
    )
    {
        int sideCount = 0;
        var queue = new Queue<Plot>();
        queue.Enqueue((initialPosition, [true, true, true, true]));

        while (queue.TryDequeue(out var current))
        {
            var (position, countPerimeterSide) = current;
            var neighbors = Deltas.Select(delta => (position.X + delta.X, position.Y + delta.Y)).ToArray();
            region.Add(position);
            MergeDuplicates(position, ref queue, ref countPerimeterSide);

            for (int i = 0; i < neighbors.Length; i++)
            {
                if (!IsOnPerimeter(neighbors[i], plantType) || region.Contains(neighbors[i]))
                {
                    countPerimeterSide[i] = true;
                }
                else
                {
                    if (countPerimeterSide[i]) sideCount++;
                    countPerimeterSide[i] = false;
                }
            }

            for (int i = 0; i < neighbors.Length; i++)
            {
                if (countPerimeterSide[i] && !region.Contains(neighbors[i]))
                {
                    queue.Enqueue((neighbors[i], [.. countPerimeterSide]));
                }
            }
        }

        return sideCount;
    }

    private static void MergeDuplicates(
        Position position,
        ref Queue<Plot> queue,
        ref bool[] countPerimeterSide
    )
    {
        var matches = queue
            .Index()
            .Where(plot => plot.Item.Position == position)
            .ToList();

        countPerimeterSide = matches
            .Select(plot => plot.Item.CountPerimeterSide)
            .Aggregate(
                countPerimeterSide,
                (resultBools, currentBools) => resultBools.Select(
                    (resultBool, index) => resultBool && currentBools[index]
                ).ToArray()
            );

        var matchingIndices = matches.Select(plot => plot.Index).ToHashSet();
        queue = new Queue<Plot>(queue.Where((_, index) => !matchingIndices.Contains(index)));
    }

}