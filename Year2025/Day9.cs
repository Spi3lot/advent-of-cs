using System.Collections;

using Tile = (int X, int Y);
using Rectangle = ((int X, int Y) FirstCorner, (int X, int Y) SecondCorner);

namespace AdventOfCode.Year2025;

using RectangleArea = (Rectangle Rectangle, long Area);

public record Day9() : AdventDay<Day9>(2025)
{

    private static readonly IComparer<RectangleArea> RectangleAreaComparer =
        Comparer<RectangleArea>.Create((a, b) => a.Area.CompareTo(b.Area));

    private readonly List<RectangleArea> _rectangleAreas = [];

    private BitArray[]? _borders;

    private SectionCollection<bool>[]? _insideShape;

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
                int index = _rectangleAreas.BinarySearch(rectangleArea, RectangleAreaComparer);
                _rectangleAreas.Insert((index < 0) ? ~index : index, rectangleArea);
            }
        }

        _rectangleAreas.Reverse();
        int width = 3 + tiles.Max(tile => tile.X); // TODO: change 3 back to 1
        int height = 3 + tiles.Max(tile => tile.Y); // TODO: change 3 back to 1
        _borders = new BitArray[height];
        _insideShape = new SectionCollection<bool>[height];

        for (int i = 0; i < height; i++)
        {
            _borders[i] = new BitArray(width);
            _insideShape[i] = new SectionCollection<bool>();
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            ConnectTiles(tiles[i], tiles[(i + 1) % tiles.Length]);
        }

        using var border = File.OpenWrite("border.txt");

        foreach (var bits in _borders)
        {
            foreach (bool bit in bits)
            {
                border.WriteByte((byte) ((bit) ? 'X' : '.'));
            }
        
            border.WriteByte((byte) '\n');
        }
        
        // foreach (var sections in _insideShape)
        // {
        //     foreach (var section in sections)
        //     {
        //         var buffer = Enumerable.Repeat((byte) ((section.Item) ? 'X' : '.'), section.Size);
        //         border.Write([..buffer]);
        //     }
        //
        //     border.WriteByte((byte) '\n');
        // }

        ConvertBorderToShape();

        using var shape = File.OpenWrite("shape.txt");

        foreach (var bits in _borders)
        {
            foreach (bool bit in bits)
            {
                shape.WriteByte((byte) ((bit) ? 'X' : '.'));
            }
        
            shape.WriteByte((byte) '\n');
        }

        // foreach (var sections in _insideShape)
        // {
        //     foreach (var section in sections)
        //     {
        //         var buffer = Enumerable.Repeat((byte) ((section.Item) ? 'X' : '.'), section.Size);
        //         border.Write([..buffer]);
        //     }
        //
        //     shape.WriteByte((byte) '\n');
        // }
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
        var coordinates = MinMax(rectangle.FirstCorner, rectangle.SecondCorner);

        for (int y = coordinates.Min.Y + 1; y < coordinates.Max.Y; y++)
        {
            for (int x = coordinates.Min.X + 1; x < coordinates.Max.X; x++)
            {
                if (!_insideShape![y][x])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void ConnectTiles(Tile firstTile, Tile secondTile)
    {
        var coordinates = MinMax(firstTile, secondTile);

        if (firstTile.X == secondTile.X)
        {
            for (int y = coordinates.Min.Y; y <= coordinates.Max.Y; y++)
            {
                _borders![y][firstTile.X] = true;
            }
        }
        else if (firstTile.Y == secondTile.Y)
        {
            for (int x = coordinates.Min.X; x <= coordinates.Max.X; x++)
            {
                _borders![firstTile.Y][x] = true;
            }
        }
        else
        {
            throw new InvalidOperationException("The attempted connection does not follow the path of a straight line");
        }
    }

    private void ConvertBorderToShape()
    {
        // TODO: distingush between 90° left and right turns
        foreach (var bits in _borders!)
        {
            bool passedBorder = false;
            bool inside = false;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                {
                    if (!passedBorder)
                    {
                        inside = !inside;
                    }

                    passedBorder = true;
                }
                else
                {
                    bits[i] = inside;
                    passedBorder = false;
                }
            }
        }
    }

    private static (Tile Min, Tile Max) MinMax(Tile firstTile, Tile secondTile)
    {
        int minX = Math.Min(firstTile.X, secondTile.X);
        int minY = Math.Min(firstTile.Y, secondTile.Y);
        int maxX = Math.Max(firstTile.X, secondTile.X);
        int maxY = Math.Max(firstTile.Y, secondTile.Y);
        return ((minX, minY), (maxX, maxY));
    }

}