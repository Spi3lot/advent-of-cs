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

        public readonly int CompareTo(Computer other)
        {
            return Name.CompareTo(other.Name);
        }
    }

}