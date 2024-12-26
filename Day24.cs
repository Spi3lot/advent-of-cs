using System.Runtime.Serialization;

namespace AdventOfCode;

public record Day24 : AdventDay<Day24>
{

    private static readonly Dictionary<string, Func<bool, bool, bool>> Operators = new()
    {
        ["XOR"] = (a, b) => a ^ b,
        ["AND"] = (a, b) => a && b,
        ["OR"] = (a, b) => a || b,
    };

    private readonly Dictionary<string, Func<bool>> _wires = [];

    public Day24()
    {
        string[] regions = Input.Split("\n\n");
        string[] initializations = regions[0].Split('\n');
        string[] gates = regions[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (string initialization in initializations)
        {
            string[] parts = initialization.Split(": ");
            string name = parts[0];
            bool value = parts[1][0] == '1';
            _wires[name] = () => value;
        }

        foreach (string gate in gates)
        {
            string operatorName = DetermineOperator(gate);
            string[] parts = gate.Split(" -> ");
            string[] inputNames = parts[0].Split($" {operatorName} ");
            string outputName = parts[1];
            var @operator = Operators[operatorName];
            _wires[outputName] = () => @operator(_wires[inputNames[0]](), _wires[inputNames[1]]());
        }
    }

    public override void SolvePart1()
    {
        ulong z = 0;

        for (int i = 0; _wires.TryGetValue(GetWireNameForBit('z', i), out var func); i++)
        {
            if (func()) z |= 1uL << i;
        }

        Console.WriteLine(z);
    }

    public override void SolvePart2() { }

    private static string DetermineOperator(string gate)
    {
        return Operators.Keys.First(gate.Contains);
    }

    private static string GetWireNameForBit(char variableName, int bit)
    {
        return $"{variableName}{((bit < 10) ? "0" : "")}{bit}";
    }

}