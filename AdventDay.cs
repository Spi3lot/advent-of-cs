namespace AdventOfCode;

public abstract record AdventDay<TSelf>(string Input) where TSelf : AdventDay<TSelf>
{

    protected AdventDay() : this(File.ReadAllText($"../../../inputs/{typeof(TSelf).Name}.txt")) { }

    public abstract void SolvePart1();

    public abstract void SolvePart2();

}