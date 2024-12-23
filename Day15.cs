namespace AdventOfCode;

public partial record Day15 : AdventDay<Day15>
{

    private readonly string _movements;

    public Day15()
    {
        string[] parts = Input.Split("\n\n");
        _movements = parts[1].Trim();

        char[][] grid = parts[0].Split('\n')
            .Select(line => line.ToCharArray())
            .ToArray();

        _singleChestRobot = new SingleChestRobot(grid.To2D());

        _doubleChestRobot = new DoubleChestRobot(new char[
            _singleChestRobot.Grid.GetLength(0),
            _singleChestRobot.Grid.GetLength(1) * 2
        ]);

        _singleChestRobot.Grid.ForEachCell((cell, i, j) =>
        {
            _doubleChestRobot.Grid[j, 2 * i] = (cell == 'O') ? '[' : cell;
            _doubleChestRobot.Grid[j, 2 * i + 1] = cell switch
            {
                'O' => ']',
                '#' => '#',
                _ => '.'
            };
        });

        _singleChestRobot.GpsCoordinates = grid
            .Select((line, j) => (
                line: line
                    .Select((char @char, int i)? (@char, i) => (@char, i))
                    .FirstOrDefault(indexedChar => indexedChar!.Value.@char == '@'),
                j
            ))
            .Where(indexedLine => indexedLine.line.HasValue)
            .Select(indexedLine => (indexedLine.line!.Value.i, indexedLine.j))
            .Single();

        _doubleChestRobot.GpsCoordinates = (2 * _singleChestRobot.GpsCoordinates.X, _singleChestRobot.GpsCoordinates.Y);
    }

    private void MoveRobot(Robot robot)
    {
        foreach (char movement in _movements)
        {
            robot.Move(movement);
        }

        Console.WriteLine(robot.SumBoxGpsCoordinates());
    }

}