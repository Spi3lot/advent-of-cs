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
        int width = 1 + tiles.Max(tile => tile.X);
        int height = 1 + tiles.Max(tile => tile.Y);
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

        MakeShapeFromBorder();
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_rectangleAreas[0]);
    }


    public override void SolvePart2()
    {
        var max = _rectangleAreas.First(x => IsInsideShape(x.Rectangle));
        Console.WriteLine(max);
    }

    public static long CalculateArea(Rectangle rectangle)
    {
        return (1 + long.Abs(rectangle.SecondCorner.X - rectangle.FirstCorner.X))
               * (1 + long.Abs(rectangle.SecondCorner.Y - rectangle.FirstCorner.Y));
    }

    public bool IsInsideShape(Rectangle rectangle)
    {
        var coordinates = MinMax(rectangle.FirstCorner, rectangle.SecondCorner);

        for (int y = coordinates.Min.Y; y <= coordinates.Max.Y; y++)
        {
            var sections = _insideShape![y];

            if (sections.GetSectionIndex(coordinates.Min.X) != sections.GetSectionIndex(coordinates.Max.X))
            {
                return false;
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

    /// TODO: fix for horizontal borders
    ///
    /// 
    /// X means the tile was correctly determined to be inside the shape, while
    /// + means it was wrongfully thought to be the case.
    /// 
    /// .......XXXX++
    /// .......XXXX..
    /// ..XXXXXXXXX..
    /// ..XXXXXXXXX..
    /// ..XXXXXXXXX..
    /// .........XX..
    /// .........XX++
    private void MakeShapeFromBorder()
    {
        foreach (var (row, bits) in _borders!.Index())
        {
            bool inside = false;
            int sectionSize = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if ((bits[i] && (i == 0 || !bits[i - 1])) || i == bits.Count - 1)
                {
                    _insideShape![row].AddSection(inside, sectionSize);
                    inside = !inside;
                    sectionSize = 0;
                }

                sectionSize++;
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