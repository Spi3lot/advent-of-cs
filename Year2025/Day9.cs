using Tile = (int X, int Y);
using Rectangle = ((int X, int Y) FirstCorner, (int X, int Y) SecondCorner);

namespace AdventOfCode.Year2025;

using RectangleArea = (Rectangle Rectangle, long Area);

public record Day9() : AdventDay<Day9>(2025)
{

    private readonly List<RectangleArea> _rectangleAreas = [];

    private bool[,] _isInsideShape = new bool[0, 0];

    public override void Setup()
    {
        var tiles = Input.Trim()
            .Split('\n')
            .Select(line => line.Split(',')
                .Select(int.Parse)
                .ToArray()
            )
            .Select(split => new Tile(split[0], split[1]))
            .ToArray();

        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = i + 1; j < tiles.Length; j++)
            {
                var rectangle = (tiles[i], tiles[j]);
                var rectangleArea = (rectangle, CalculateArea(rectangle));
                int index = ~_rectangleAreas.BinarySearch(rectangleArea);
                _rectangleAreas.Insert(index, rectangleArea);
            }
        }
        
        // TODO: fill _isInsideShape

        _rectangleAreas.Reverse();
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_rectangleAreas[0].Area);
    }


    public override void SolvePart2()
    {
        var max = _rectangleAreas.First(x => IsInsideShape(x.Rectangle));
        Console.WriteLine(max.Area);
    }

    public static long CalculateArea(Rectangle rectangle)
    {
        return (1 + long.Abs(rectangle.SecondCorner.X - rectangle.FirstCorner.X))
               * (1 + long.Abs(rectangle.SecondCorner.Y - rectangle.FirstCorner.Y));
    }

    public bool IsInsideShape(Rectangle rectangle)
    {
        return _isInsideShape[rectangle.FirstCorner.X, rectangle.FirstCorner.Y];
    }

}