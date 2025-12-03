using System.Runtime.Intrinsics;

namespace AdventOfCode.Year2024;

public partial record Day14 : AdventDay<Day14>
{

    public override void SolvePart2()
    {
        InitializeRobots();
        int second;

        /// Period of length 10403
        /// if (second % 10403 == 7132) christmasTree = true
        /// Assuming one uses the 'good' modulo, otherwise
        /// if ((second % 10403) + 10403 == 7132) christmasTree = true
        for (second = 0; !DoRobotsFormChristmasTree(); second++)
        {
            foreach (var robot in _robots)
            {
                robot.MoveForOneSecond();
                robot.Clamp(_gridDimensions);
            }
        }

        PrintRobotGrid();
        Console.WriteLine(second);
    }

    private bool DoRobotsFormChristmasTree()
    {
        return _robots.Any(robot => IsRobotPartOfChristmasTree(robot, 10));  // Exact height is 33 but 10 was my guess. As it turns out, 10 is actually the lowest number that works.
    }

    private bool IsRobotPartOfChristmasTree(Robot robotToCheck, int height)
    {
        for (long dy = 0; dy < height; dy++)
        {
            var offsetPosition = Vector128.Create(robotToCheck.Position[0], robotToCheck.Position[1] + dy);

            if (!_robots.Any(robot => robot.Position == offsetPosition))
            {
                return false;
            }

        }

        return true;
    }

}