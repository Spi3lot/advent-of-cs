using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode;

public partial record Day24
{

    private sealed class LogicGate(string type, string input1, string input2) : Wire, IParsable<LogicGate>
    {

        private static readonly Dictionary<string, Func<bool, bool, bool>> Operators = new()
        {
            ["XOR"] = (a, b) => a ^ b,
            ["AND"] = (a, b) => a && b,
            ["OR"] = (a, b) => a || b,
        };

        private readonly Func<bool, bool, bool> _operator = Operators[type];

        public override bool Output => _operator(Wires[input1].Output, Wires[input2].Output);

        public override int Depth => 1 + Math.Min(Wires[input1].Depth, Wires[input2].Depth);

        public static LogicGate Parse(string s, IFormatProvider? provider)
        {
            string operatorName = DetermineOperator(s);
            string[] inputNames = s.Split($" {operatorName} ");
            return new LogicGate(operatorName, inputNames[0], inputNames[1]);
        }

        public static bool TryParse(
            [NotNullWhen(true)] string? s,
            IFormatProvider? provider,
            [MaybeNullWhen(false)] out LogicGate result
        )
        {
            try
            {
                result = Parse(s!, provider);
                return true;
            }
            catch
            {
                result = null;
                Console.WriteLine(result);
                return false;
            }
        }

        private static string DetermineOperator(string gateString)
        {
            return Operators.Keys.First(gateString.Contains);
        }

    }

}