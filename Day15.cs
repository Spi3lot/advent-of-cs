namespace AdventOfCode;

public partial record Day15
{

    private string? _movements;

    private void Initialize()
    {
        string[] parts = Input.Split("\n\n");
        _movements = parts[1].Trim();

        char[][] grid = parts[0].Split('\n')
            .Select(line => line.ToCharArray())
            .ToArray();

        Part1.Bot.Grid = JaggedTo2D(grid);

        // TODO: fill
        Part2.Bot.Grid = new char[
            Part1.Bot.Grid.GetLength(0),
            Part1.Bot.Grid.GetLength(1)
        ];

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


        Part2.Bot.GpsCoordinates = (2 * Part1.Bot.GpsCoordinates.X, 2 * Part1.Bot.GpsCoordinates.Y);
    }

    private static T[,] JaggedTo2D<T>(T[][] source)
    {
        try
        {
            int dim0 = source.Length;

            int dim1 = source.GroupBy(row => row.Length).Single()
                .Key; // throws InvalidOperationException if source is not rectangular

            var result = new T[dim0, dim1];

            for (int i = 0; i < dim0; ++i)
            {
                for (int j = 0; j < dim1; ++j)
                {
                    result[i, j] = source[i][j];
                }
            }

            return result;
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("The given jagged array is not rectangular.");
        }
    }

}