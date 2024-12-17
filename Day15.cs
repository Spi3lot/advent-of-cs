using System.Runtime.Intrinsics;

namespace AdventOfCode;

public record Day15 : AdventDay<Day15>
{

    private readonly string[] _grid;

    private readonly string _movements;

    private readonly (int X, int Y) _robotPosition;

    public Day15()
    {
        string[] parts = Input.Split("\n\n");
        _grid = parts[0].Split('\n');
        _movements = parts[1].Trim();

        _robotPosition = _grid
            .Select((line, j) => (
                line
                    .Select((char @char, int i)? (@char, i) => (@char, i))
                    .Where(indexedChar => indexedChar != null)
                    .First(indexedChar => indexedChar!.Value.@char == '@')
                    .Value
                    .i,
                j
            ))
            .First();

        Console.WriteLine(_robotPosition);
    }

    public override void SolvePart1()
    {
        Console.WriteLine(_movements);
    }

    public override void SolvePart2() { }

}