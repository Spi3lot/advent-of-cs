namespace AdventOfCode;

public partial record Day23 : AdventDay<Day23>
{

    private readonly record struct Computer(string Name) : IComparable<Computer>
    {

        public static readonly Dictionary<Computer, ISet<Computer>> Connections = [];

        public readonly void ConnectTo(Computer that)
        {
            if (Connections.TryGetValue(this, out var theseConnections))
            {
                theseConnections.Add(that);
            }
            else
            {
                HashSet<Computer> connections = [that];
                Connections[this] = connections;
            }

            if (Connections.TryGetValue(that, out var thoseConnections))
            {
                thoseConnections.Add(this);
            }
            else
            {
                HashSet<Computer> connections = [this];
                Connections[that] = connections;
            }
        }

        public static HashSet<Interconnection> GetTriangularInterconnections()
        {
            HashSet<Interconnection> interconnections = [];

            foreach (var (connector, connectionSet) in Connections)
            {
                var connectionList = connectionSet.ToList();

                for (int i = 0; i < connectionList.Count; i++)
                {
                    for (int j = i + 1; j < connectionList.Count; j++)
                    {
                        if (Connections[connectionList[i]].Contains(connectionList[j]))
                        {
                            interconnections.Add([connector, connectionList[i], connectionList[j]]);
                        }
                    }
                }
            }

            return interconnections;
        }

        public readonly ICollection<Interconnection> GetInterconnections(int depth)
        {
            ICollection<Interconnection> interconnections = [];
            GetInterconnections(depth, this, [], [this], interconnections);
            return interconnections;
        }

        public readonly void GetInterconnections(
            int depth,
            Computer origin,
            HashSet<Computer> visited,
            Interconnection currentInterconnection,
            ICollection<Interconnection> interconnections
        )
        {
            foreach (var connection in Connections[this])
            {
                if (visited.Contains(connection)) continue;

                // 2 because we start 1 step ahead and
                // know when to stop 1 step before the end
                if (connection == origin)
                {
                    if (depth == 2)
                    {
                        interconnections.Add(currentInterconnection);
                        return;
                    }

                    continue;
                }

                connection.GetInterconnections(
                    depth - 1,
                    origin,
                    [.. visited, connection],
                    [.. currentInterconnection, connection],
                    interconnections
                );
            }
        }

        public int CompareTo(Computer other)
        {
            return Name.CompareTo(other.Name);
        }
    }

}