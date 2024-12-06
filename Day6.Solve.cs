namespace AdventOfCode;

public partial record Day6(string Input) : AdventDay(Input)
{

    public override void SolvePart1()
    {
        var grid = Input.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line
                .Select(static gridElement => ParseGridFlags(gridElement))
                .ToArray())
            .ToArray();

        var guard = new Guard(grid);
        int count = 1;

        while (guard.Move() && !guard.WasInCurrentSituationBefore())
        {
            if (!guard.VisitedCurrentPositionAlready())
            {
                count++;
            }

            guard.UpdateFlagsForCurrentPosition();
        }

        Console.WriteLine(count);
    }

    public override void SolvePart2()
    {

    }


}