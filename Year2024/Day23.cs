namespace AdventOfCode.Year2024;

public partial record Day23 : AdventDay<Day23>
{

    public Day23() {
        var computerPairs = Input.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split('-').Select(name => new Computer(name)).ToArray())
            .Select(computers => (computers[0], computers[1]));

        foreach (var (computer1, computer2) in computerPairs)
        {
            computer1.ConnectTo(computer2);
        }
    }

    public override void SolvePart1()
    {
        int count = Computer.GetTriangularInterconnections()
            .Count(interconnection => interconnection.Any(computer => computer.Name.StartsWith('t')));

        Console.WriteLine(count);
    }

    public override void SolvePart2()
    {
        Console.WriteLine(Computer.FindCliques().MaxBy(clique => clique.Count));
    }

}