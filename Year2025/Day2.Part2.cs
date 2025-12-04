namespace AdventOfCode.Year2025;

public partial record Day2
{

    public override void SolvePart2()
    {
        long sum = Input.Split(',')
            .Select(range => range.Split('-'))
            .Select(range => range.Select(long.Parse).ToArray())
            .Select(range => LongRange(range[0], range[1]))
            .Sum(range => range.Where(IsIdRepetitive).Sum());

        Console.WriteLine(sum);
    }

    private static bool IsIdRepetitive(long id)
    {
        string str = id.ToString();

        for (int chunkSize = 1; chunkSize <= str.Length / 2; chunkSize++)
        {
            int distinctChunkCount = str.Chunk(chunkSize)
                .DistinctBy(chars => new string(chars))
                .Count();
            
            if (distinctChunkCount == 1)
            {
                return true;
            }
        }

        return false;
    }

}