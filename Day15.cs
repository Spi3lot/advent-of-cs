using System;

namespace AdventOfCode;

public partial record Day15 : AdventDay<Day15>
{

    private readonly Robot _robot = new();

    private string? _movements;

    private void Initialize()
    {
        string[] parts = Input.Split("\n\n");
        _movements = parts[1].Trim();

        char[][] grid = parts[0].Split('\n')
            .Select(line => line.ToCharArray())
            .ToArray();
            
        _robot.Grid = JaggedTo2D(grid);

        _robot.GpsCoordinates = grid
            .Select((line, j) => (
                line: line
                    .Select((char @char, int i)? (@char, i) => (@char, i))
                    .FirstOrDefault(indexedChar => indexedChar!.Value.@char == '@'),
                j
            ))
            .Where(indexedLine => indexedLine.line.HasValue)
            .Select(indexedLine => (indexedLine.line!.Value.i, indexedLine.j))
            .Single();
    }
    
    public override void SolvePart1()
    {
        Initialize();

        foreach (char movement in _movements!)
        {
            _robot.Move(movement);
        }

        Console.WriteLine(_robot.SumBoxGpsCoordinates());
    }

    public override void SolvePart2() { }

    internal static T[,] JaggedTo2D<T>(T[][] source)
    {
        try
        {
            int FirstDim = source.Length;
            int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

            var result = new T[FirstDim, SecondDim];
            for (int i = 0; i < FirstDim; ++i)
                for (int j = 0; j < SecondDim; ++j)
                    result[i, j] = source[i][j];

            return result;
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("The given jagged array is not rectangular.");
        }
    }

}