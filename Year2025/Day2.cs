namespace AdventOfCode.Year2025;

public partial record Day2() : AdventDay<Day2>(2025)
{
    
    private static IEnumerable<long> LongRange(long start, long stopInclusive)
    {
        for (long i = start; i <= stopInclusive; i++)
        {
            yield return i;
        }
    }

}