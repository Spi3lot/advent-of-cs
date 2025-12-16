namespace AdventOfCode.Year2025;

using JunctionBox = (int X, int Y, int Z);

public partial record Day8() : AdventDay<Day8>(2025)
{

    public override void SolvePart1()
    {
        var boxes = Input.Trim()
            .Split('\n')
            .Select(line => line.Split(',')
                .Select(int.Parse)
                .ToArray()
            )
            .Select(ints => (ints[0], ints[1], ints[2]))
            .ToHashSet();

        var circuits = new Circuits(boxes);
        var excludedPairs = new HashSet<(JunctionBox, JunctionBox)>();

        for (int i = 0; i < 10; i++)
        {
            if (circuits.TryGetClosestBoxPair(boxes, excludedPairs, out var closest))
            {
                circuits.Connect(closest.Pair.First, closest.Pair.Second);
            }

            excludedPairs.Add(closest.Pair);
            excludedPairs.Add((closest.Pair.Second, closest.Pair.First));
            Console.WriteLine(i);
        }

        Console.WriteLine(circuits.Score());
    }


    public override void SolvePart2()
    {
    }

    public static double Distance(JunctionBox from, JunctionBox to)
    {
        int x = to.X - from.X;
        int y = to.Y - from.Y;
        int z = to.Z - from.Z;
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public class Circuits
    {

        private readonly Dictionary<JunctionBox, int> _boxCircuits = [];

        private readonly Dictionary<int, List<JunctionBox>> _circuitBoxes = [];

        public Circuits(IEnumerable<JunctionBox> boxes)
        {
            boxes.Index()
                .ToList()
                .ForEach(box =>
                {
                    _boxCircuits[box.Item] = box.Index;
                    _circuitBoxes[box.Index] = [box.Item];
                });
        }

        public bool TryGetClosestBoxPair(
            HashSet<JunctionBox> boxes,
            HashSet<(JunctionBox, JunctionBox)> excludedPairs,
            out (double Distance, (JunctionBox First, JunctionBox Second) Pair) closest
        )
        {
            closest = (double.PositiveInfinity, default);
            
            foreach (var box1 in boxes)
            {
                foreach (var box2 in boxes)
                {
                    if (box1 == box2 || excludedPairs.Contains((box1, box2)))
                    {
                        continue;
                    }

                    if (AreConnected(box1, box2))
                    {
                        continue;
                    }

                    double distance = Distance(box1, box2);

                    if (distance < closest.Distance)
                    {
                        closest = (distance, (box1, box2));
                    }
                }
            }
            
            return false;
        }

        public bool AreConnected(JunctionBox from, JunctionBox to)
        {
            return _boxCircuits[from] == _boxCircuits[to];
        }

        public void Connect(JunctionBox from, JunctionBox to)
        {
            int fromCircuit = _boxCircuits[from];
            int toCircuit = _boxCircuits[to];
            int oldCircuit = Math.Max(fromCircuit, toCircuit);
            int newCircuit = Math.Min(fromCircuit, toCircuit);
            _boxCircuits[from] = newCircuit;
            _boxCircuits[to] = newCircuit;
            _circuitBoxes[newCircuit].Add(to);
            _circuitBoxes[oldCircuit].Remove(to);

            if (_circuitBoxes[oldCircuit].Count == 0)
            {
                _circuitBoxes.Remove(oldCircuit);
            }
        }

        public ulong Score()
        {
            return _circuitBoxes.Values
                .Select(circuit => (ulong) circuit.Count)
                .OrderDescending()
                .Take(3)
                .Aggregate(1uL, (result, current) => checked(result * current));
        }

    }

}