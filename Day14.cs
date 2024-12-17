using System.Runtime.Intrinsics;

namespace AdventOfCode;

public partial record Day14 : AdventDay<Day14>
{

    private static readonly Vector128<long> _gridDimensions = Vector128.Create(101, 103);

    private readonly List<Robot> _robots = [];

    private void InitializeRobots()
    {
        _robots.Clear();

        foreach (var robot in Input.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).Select(Robot.Parse))
        {
            _robots.Add(robot);
        }
    }

    private void PrintRobotGrid()
    {
        for (long j = 0; j < _gridDimensions[1]; j++)
        {
            for (long i = 0; i < _gridDimensions[0]; i++)
            {
                int count = _robots.Count(robot => robot.Position == Vector128.Create(i, j));
                Console.Write((count > 0) ? ((count > 9) ? $"({count})" : $"{count}"[0]) : ".");
            }

            Console.WriteLine();
        }
    }

    private static int GetQuadrant(Robot robot)
    {
        if (robot.Position[0] == _gridDimensions[0] / 2 || robot.Position[1] == _gridDimensions[1] / 2) return -1;
        long x = 2 * robot.Position[0] / _gridDimensions[0];
        long y = 2 * robot.Position[1] / _gridDimensions[1];
        return (int) ((y << 1) | x);
    }

}