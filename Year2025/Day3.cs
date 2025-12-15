namespace AdventOfCode.Year2025;

public partial record Day3() : AdventDay<Day3>(2025)
{

    public override void SolvePart1()
    {
        int totalOutputJoltage = Input.Split('\n')
            .Where(str => !string.IsNullOrWhiteSpace(str))
            .Select(str => new BatteryBank(str))
            .Select(bank => bank.FindMaximumJoltageFor2Batteries())
            .Sum();

        Console.WriteLine(totalOutputJoltage);
    }

    public override void SolvePart2()
    {
        long totalOutputJoltage = Input.Split('\n')
            .Where(str => !string.IsNullOrWhiteSpace(str))
            .Select(str => new BatteryBank(str))
            .Select(bank => bank.FindMaximumJoltageForNBatteries(12))
            .Sum();

        Console.WriteLine(totalOutputJoltage);
    }

}