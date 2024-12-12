namespace AdventOfCode;
public partial record Day9 : AdventDay<Day9>
{
    public override void SolvePart1()
    {
        var disk = Disk.FromMap(Input);
        disk.FragmentBlocks();
        disk.RecalculateFragments();
        Console.WriteLine(disk.CalculateChecksumFast());
    }

    public override void SolvePart2()
    {
        var disk = Disk.FromMap(Input);
        disk.SqueezeFiles();
        Console.WriteLine(disk.CalculateChecksumFast());
    }

}
