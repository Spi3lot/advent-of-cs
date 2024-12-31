namespace AdventOfCode;

public partial record Day24 : AdventDay<Day24>
{

    public Day24()
    {
        string[] regions = Input.Split("\n\n");
        string[] initializations = regions[0].Split('\n');
        string[] gates = regions[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (string initialization in initializations)
        {
            string[] parts = initialization.Split(": ");
            string name = parts[0];
            Wire.Connect(name, ConstantWire.Parse(parts[1], null));
        }

        foreach (string gateString in gates)
        {
            string[] parts = gateString.Split(" -> ");
            string outputName = parts[1];
            Wire.Connect(outputName, LogicGate.Parse(parts[0], null));
        }
    }

    public override void SolvePart1()
    {
        Console.WriteLine(Wire.GetValueForVariable('z'));
    }

    public override void SolvePart2()
    {
        var wireDepths = Wire.Wires.ToLookup(wire => wire.Value.Depth);

        var depths = from wire in wireDepths
                     let depth = wire.Key
                     orderby depth
                     select depth;

        var enumerators = (from depth in depths
                           let wireDepth = wireDepths[depth]
                           select wireDepth.GetEnumerator())
                           .ToList();

        IEnumerable<bool> haveNext = enumerators.Select(_ => true);

        while ((haveNext = MoveAllNext(enumerators, haveNext)).Any(hasNext => hasNext))
        {
            foreach (var (enumerator, hasNext) in enumerators.Zip(haveNext))
            {
                Console.Write((hasNext) ? $"{enumerator.Current.Key,-4}" : "    ");
            }
        }
    }

    private static IEnumerable<bool> MoveAllNext<T>(
        IEnumerable<IEnumerator<T>> enumerators,
        IEnumerable<bool> didHaveNext
    )
    {
        foreach (var (enumerator, hadNext) in enumerators.Zip(didHaveNext))
        {
            yield return hadNext && enumerator.MoveNext();
        }
    }

}