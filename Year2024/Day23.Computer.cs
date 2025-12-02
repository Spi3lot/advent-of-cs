namespace AdventOfCode.Year2024;

public partial record Day23
{

    private readonly record struct Computer(string Name) : IComparable<Computer>
    {

        private static readonly Dictionary<Computer, Interconnection> Connections = [];

        public void ConnectTo(Computer that)
        {
            if (Connections.TryGetValue(this, out var theseConnections))
            {
                theseConnections.Add(that);
            }
            else
            {
                Interconnection connections = [that];
                Connections[this] = connections;
            }

            if (Connections.TryGetValue(that, out var thoseConnections))
            {
                thoseConnections.Add(this);
            }
            else
            {
                Interconnection connections = [this];
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

        public static IEnumerable<Interconnection> FindCliques()
        {
            return BronKerbosch([], [..Connections.Keys], []);
        }

        private static IEnumerable<Interconnection> BronKerbosch(
            Interconnection r,
            Interconnection p,
            Interconnection x
        )
        {
            if (p.Count <= 0 && x.Count <= 0) yield return r;

            foreach (var v in p)
            {
                var cliques = BronKerbosch(
                    [..r.Union([v])],
                    [..p.Intersect(Connections[v])],
                    [..x.Intersect(Connections[v])]
                    );
                
                foreach (var clique in cliques) yield return clique;
                p.Remove(v);
                x.Add(v);
            }
        }

        public int CompareTo(Computer other)
        {
            return string.CompareOrdinal(Name, other.Name);
        }

    }

}