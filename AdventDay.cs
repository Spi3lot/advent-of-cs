namespace AdventOfCode;

public abstract record AdventDay<TSelf>(string Input) where TSelf : AdventDay<TSelf>
{

    protected AdventDay(int year = 2024) : this(File.ReadAllText($"../../../Year{year}/inputs/{typeof(TSelf).Name}.txt"))
    {
    }

    public virtual void Setup()
    {
    }

    public abstract void SolvePart1();

    public abstract void SolvePart2();

}