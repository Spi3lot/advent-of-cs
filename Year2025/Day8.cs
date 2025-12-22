namespace AdventOfCode.Year2025;

public partial record Day8 : AdventDay<Day8>
{

    private readonly List<JunctionBox> _boxes = [];

    private readonly List<(double Dist, JunctionBox First, JunctionBox Second)> _pairs = [];

    public Day8() : base(2025)
    {
        _boxes = Input.Trim()
            .Split('\n')
            .Select(line => line.Split(',')
                .Select(int.Parse)
                .ToArray()
            )
            .Select(JunctionBox.FromArray)
            .ToList();

        for (int i = 0; i < _boxes.Count; i++)
        {
            for (int j = i + 1; j < _boxes.Count; j++)
            {
                _pairs.Add((_boxes[i].DistanceTo(_boxes[j]), _boxes[i], _boxes[j]));
            }
        }

        _pairs = _pairs.OrderBy(pair => pair.Dist).ToList();
    }

    public override void SolvePart1()
    {
        var circuits = new Circuits(_boxes);

        _pairs.Take(1000)
            .ToList()
            .ForEach(pair => circuits.Connect(pair.First, pair.Second));

        Console.WriteLine(circuits.Score());
    }


    public override void SolvePart2()
    {
        var circuits = new Circuits(_boxes);

        foreach (var pair in _pairs.TakeWhile(_ => circuits.CircuitCount > 1))
        {
            circuits.Connect(pair.First, pair.Second);
        }

        Console.WriteLine(circuits.LastConnection.From.X * circuits.LastConnection.To.X);
    }

}