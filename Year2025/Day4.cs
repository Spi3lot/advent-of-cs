namespace AdventOfCode.Year2025;

public partial record Day4() : AdventDay<Day4>(2025)
{

    public override void SolvePart1()
    {
        var grid = new PaperGrid(Input);

        int code = grid.PaperNeighborCounts
            .Cast<int>()
            .Count(neighborCount => neighborCount < 4);
        
        grid.PaperNeighborCounts.Print(" ");
        
        Console.WriteLine(code);
    }
    
    

    public override void SolvePart2()
    {
        
        Console.WriteLine(0);
    }

}