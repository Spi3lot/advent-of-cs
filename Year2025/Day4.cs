namespace AdventOfCode.Year2025;

public partial record Day4() : AdventDay<Day4>(2025)
{

    public override void SolvePart1()
    {
        int accessiblePaperRollCount = new PaperGrid(Input)
            .AccessiblePaperRolls()
            .Count();

        Console.WriteLine(accessiblePaperRollCount);
    }


    public override void SolvePart2()
    {
        var grid = new PaperGrid(Input);
        int totalCount = 0;
        int currentCount;

        do
        {
            var accessiblePaperRolls = grid.AccessiblePaperRolls().ToList();
            accessiblePaperRolls.ForEach(index => grid.InformPaperNeighborsOfAbsence(index.X, index.Y));
            currentCount = accessiblePaperRolls.Count;
            totalCount += currentCount;
        } while (currentCount > 0);

        Console.WriteLine(totalCount);
    }

}