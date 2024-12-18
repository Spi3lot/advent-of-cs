namespace AdventOfCode;

public partial record Day6 : AdventDay<Day6>
{

    private readonly CellConditions[][] _grid;

    private readonly Guard _guard;

    public Day6(string input) : base(input)
    {
        _grid = input.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line
                .Select(static gridElement => ParseGridFlags(gridElement))
                .ToArray())
            .ToArray();

        _guard = new Guard(_grid);
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_guard.PatrolAndCountUniquePositions());
        _guard.Reset();
    }

    public override void SolvePart2()
    {
        int possibleObstaclePlacementCount = 0;

        for (int j = 0; j < _grid.Length; j++)
        {
            for (int i = 0; i < _grid[j].Length; i++)
            {
                if (_grid[j][i] != CellConditions.None) continue;
                _guard.Grid[j][i] = CellConditions.Obstacle;

                if (_guard.PatrolAndCountUniquePositions() == null)
                {
                    possibleObstaclePlacementCount++;
                }

                _guard.Reset();
            }
        }

        Console.WriteLine(possibleObstaclePlacementCount);
    }

}