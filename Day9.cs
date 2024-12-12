namespace AdventOfCode;
public partial record Day9 : AdventDay<Day9>
{
    public override void SolvePart1()
    {
        var disk = Disk.FromMap(Input);
        disk.FragmentBlocks();
        disk.RecalculateFragments();
        Console.WriteLine(disk.CalculateChecksum());
        Console.WriteLine(disk.CalculateChecksumFast());
        Console.WriteLine(disk);
    }

    public override void SolvePart2()
    {
        var disk = Disk.FromMap(Input);
        Console.WriteLine(disk);
        disk.SqueezeFiles();
        disk.RecalculateBlocks();
        Console.WriteLine(disk);
        Console.WriteLine(disk.CalculateChecksum());
        Console.WriteLine(disk.CalculateChecksumFast());
    }

}
