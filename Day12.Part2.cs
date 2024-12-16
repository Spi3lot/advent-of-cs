namespace AdventOfCode;

public partial record Day12
{

    private int CalcAreaSideCountFencingPriceForRegion((int X, int Y) position, bool[,] covered)
    {
        HashSet<(int X, int Y)> region = [];
        int perimeter = GetRegionSideCount(_farm[position.Y][position.X], region, position);

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
        (int X, int Y) initialPosition
    )
    {
        int sideCount = 0;
        var queue = new Queue<((int X, int Y) Position, bool[] CountPerimeterSide)>();
        queue.Enqueue((initialPosition, [true, true, true, true]));

        while (queue.TryDequeue(out var current))
        {
            var (position, countPerimeterSide) = current;
            var neighbors = Deltas.Select(delta => (position.X + delta.X, position.Y + delta.Y)).ToArray();
            region.Add(position);

            var matches = queue
                .Select((plot, index) => (Value: plot, Index: index))
                .Where(plot => plot.Value.Position == position);

            var matchingIndices = matches.Select(plot => plot.Index).ToHashSet();

            countPerimeterSide = matches
                .Select(plot => plot.Value.CountPerimeterSide)
                .Aggregate(countPerimeterSide,
                    (resultBools, currentBools) => resultBools
                        .Select((resultBool, index) => resultBool && currentBools[index]).ToArray());

            queue = new Queue<((int, int), bool[])>(queue.Where((plot, index) => !matchingIndices.Contains(index)));

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
                    queue.Enqueue((neighbors[i], [..countPerimeterSide]));
                }
            }
        }

        return sideCount;
    }

}