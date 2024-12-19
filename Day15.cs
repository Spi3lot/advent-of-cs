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

        Part1.Bot.Grid = grid.To2D();

        Part2.Bot.Grid = new char[
            Part1.Bot.Grid.GetLength(0),
            Part1.Bot.Grid.GetLength(1) * 2
        ];

        Part1.Bot.Grid.ForEachCell((cell, i, j) =>
        {
            Part2.Bot.Grid[j, 2 * i] = (cell == 'O') ? '[' : cell;
            Part2.Bot.Grid[j, 2 * i + 1] = (cell == 'O') ? ']' : ((cell == '#') ? '#' : '.');
        });

        Part1.Bot.GpsCoordinates = grid
            .Select((line, j) => (
                line: line
                    .Select((char @char, int i)? (@char, i) => (@char, i))
                    .FirstOrDefault(indexedChar => indexedChar!.Value.@char == '@'),
                j
            ))
            .Where(indexedLine => indexedLine.line.HasValue)
            .Select(indexedLine => (indexedLine.line!.Value.i, indexedLine.j))
            .Single();


        Part2.Bot.GpsCoordinates = (2 * Part1.Bot.GpsCoordinates.X, Part1.Bot.GpsCoordinates.Y);
    }

    private void SolvePart(RobotBase robot)
    {
        foreach (char movement in _movements)
        {
            robot.Move(movement);
        }

        Console.WriteLine(robot.SumBoxGpsCoordinates());
    }

}