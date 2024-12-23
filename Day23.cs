using System;

namespace AdventOfCode;

public record Day23 : AdventDay<Day23>
{

    public override void SolvePart1()
    {
        var computerPairs = Input.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split('-').Select(name => new Computer(name)).ToArray())
            .Select(computers => (computers[0], computers[1]));

        foreach (var (computer1, computer2) in computerPairs)
        {
            computer1.ConnectTo(computer2);
        }

        foreach (var (computer, connections) in Computer.Connections)
        {
            Console.Write($"{computer.Name}: ");

            foreach (var connection in connections)
            {
                Console.Write($"{connection.Name} ");
            }

            Console.WriteLine();
        }
    }

    public override void SolvePart2() { }

    private readonly record struct Computer(string Name)
    {

        public static readonly Dictionary<Computer, ICollection<Computer>> Connections = [];

        public readonly void ConnectTo(Computer that)
        {
            if (Connections.TryGetValue(this, out var theseConnections))
            {
                theseConnections.Add(that);
            }
            else
            {
                ICollection<Computer> newSet = [that];
                Connections[this] = newSet;
            }
            
            if (Connections.TryGetValue(that, out var thoseConnections))
            {
                thoseConnections.Add(this);
            }
            else
            {
                ICollection<Computer> newSet = [this];
                Connections[that] = newSet;
            }
        }

    }

}