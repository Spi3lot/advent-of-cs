namespace AdventOfCode.Year2024;

public partial record Day13 : AdventDay<Day13>
{


    public override void SolvePart1()
    {
        long minimumTokenCount = Input.Split("\n\n")
            .Select(ClawMachine.Parse)
            .Sum(clawMachine => clawMachine.CalcMinimumTokenCountForPrize());

        Console.WriteLine(minimumTokenCount);
    }

    public override void SolvePart2()
    {
        long minimumTokenCount = Input.Split("\n\n")
            .Select(ClawMachine.Parse)
            .Select(clawMachine =>
            {
                clawMachine.Prize.X += 10_000_000_000_000;
                clawMachine.Prize.Y += 10_000_000_000_000;
                return clawMachine;
            })
            .Sum(clawMachine => clawMachine.CalcMinimumTokenCountForPrize());

        Console.WriteLine(minimumTokenCount);
    }

}