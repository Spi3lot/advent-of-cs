namespace AdventOfCode;
public partial record Day9 : AdventDay<Day9>
{
    public override void SolvePart1()
    {
        var disk = Disk.FromMap(Input);
        disk.Defragment();
        Console.WriteLine(disk.CalculateChecksum());
    }

    public override void SolvePart2()
    {

    }

}
