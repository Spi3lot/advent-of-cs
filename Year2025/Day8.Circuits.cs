namespace AdventOfCode.Year2025;

public partial record Day8
{

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
        
        public int CircuitCount => _circuitBoxes.Count;

        public (JunctionBox From, JunctionBox To) LastConnection { get; private set; }

        public bool Connect(JunctionBox from, JunctionBox to)
        {
            if (AreConnected(from, to))
            {
                return false;
            }

            int fromCircuit = _boxCircuits[from];
            int toCircuit = _boxCircuits[to];
            int oldCircuit = Math.Max(fromCircuit, toCircuit);
            int newCircuit = Math.Min(fromCircuit, toCircuit);
            _circuitBoxes[oldCircuit].ForEach(oldBox => _boxCircuits[oldBox] = newCircuit);
            _circuitBoxes[newCircuit].AddRange(_circuitBoxes[oldCircuit]);
            _circuitBoxes.Remove(oldCircuit);
            LastConnection = (from, to);
            return true;
        }

        public bool AreConnected(JunctionBox from, JunctionBox to)
        {
            return _boxCircuits[from] == _boxCircuits[to];
        }

        public ulong Score()
        {
            return _circuitBoxes.Values
                .Select(circuit => (ulong) circuit.Count)
                .OrderDescending()
                .Take(3)
                .Aggregate(1uL, (result, current) => result * current);
        }

    }

}