namespace AdventOfCode.Year2025;

public record Day1() : AdventDay<Day1>(2025)
{

    public override void SolvePart1()
    {
        var dial = new Dial();
        
        int code = Input.Split('\n')
            .Where(amount => !string.IsNullOrWhiteSpace(amount))
            .Select(dial.Rotate)
            .Count(point => point == 0);
        
        Console.WriteLine(code);
    }
    
    

    public override void SolvePart2()
    {
        var dial = new Dial();
        
        int code = Input.Split('\n')
            .Where(amount => !string.IsNullOrWhiteSpace(amount))
            .SelectMany(dial.RotateIncrementally)
            .Count(point => point == 0);
        
        Console.WriteLine(code);
    }

}