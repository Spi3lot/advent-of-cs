using System.Runtime.Intrinsics;

namespace AdventOfCode.Year2024;

public partial record Day14 : AdventDay<Day14>
{

    public override void SolvePart1()
    {
        InitializeRobots();

        foreach (var robot in _robots)
        {
            robot.Move(100);
            robot.Clamp(_gridDimensions);
        }

        Console.WriteLine(CalcSecurityFactor());
    }

    private long CalcSecurityFactor()
    {
        Dictionary<int, long> robotCounts = [];

        foreach (var robot in _robots)
        {
            int quadrant = GetQuadrant(robot);
            robotCounts[quadrant] = 1 + robotCounts.GetValueOrDefault(quadrant);
        }

        return robotCounts
            .Where(robotCount => robotCount.Key >= 0)
            .Aggregate(1L, (securityFactor, robotCount) => securityFactor * robotCount.Value);
    }

}