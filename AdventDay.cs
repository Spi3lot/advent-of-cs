namespace AdventOfCode;

public abstract record AdventDay<T>(string Input) where T : AdventDay<T>
{

    protected AdventDay() : this(File.ReadAllText($"../../../inputs/{typeof(T).Name}.txt")) { }

    public abstract void SolvePart1();

    public abstract void SolvePart2();

}