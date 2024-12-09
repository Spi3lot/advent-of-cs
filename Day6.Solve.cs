﻿namespace AdventOfCode;

public partial record Day6 : AdventDay
{

    private readonly GridFlag[][] _grid;

    private readonly Guard _guard;

    public Day6(string input) : base(input)
    {
        _grid = input.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line
                .Select(static gridElement => ParseGridFlags(gridElement))
                .ToArray())
            .ToArray();

        _guard = new(_grid);
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
                if (_grid[j][i] == GridFlag.Empty)
                {
                    _guard.Grid[j][i] = GridFlag.Obstacle;

                    if (_guard.PatrolAndCountUniquePositions() == null)
                    {
                        possibleObstaclePlacementCount++;
                    }

                    _guard.Reset();
                }
            }
        }

        Console.WriteLine(possibleObstaclePlacementCount);
    }

}