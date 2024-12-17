namespace AdventOfCode;

public partial record Day13 : AdventDay<Day13>
{


    public override void SolvePart1()
    {
        int minimumTokenCount = Input.Split("\n\n")
            .Select(ClawMachine.Parse)
            .Select(clawMachine => clawMachine.CalcMinimumTokenCountForPrize())
            .Sum();

        Console.WriteLine(minimumTokenCount);
    }

    public override void SolvePart2()
    {
        
    }

}