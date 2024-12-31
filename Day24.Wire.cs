using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode;

public partial record Day24
{

    private abstract class Wire
    {

        public abstract bool Value { get; }

        public abstract int Depth { get; }

        public static readonly Dictionary<string, Wire> Wires = [];

        public static void Connect(string name, Wire wire) => Wires.Add(name, wire);

        public static ulong GetValueForVariable(char variableName)
        {
            ulong variable = 0;

            for (byte i = 0; TryGetWireForBit(variableName, i, out var wire); i++)
            {
                if (wire.Value) variable |= 1uL << i;
            }

            return variable;
        }

        public static bool TryGetWireForBit(
            char variableName,
            byte bit,
            [MaybeNullWhen(false)] out Wire wire
        )
        {
            return Wires.TryGetValue(GetWireNameForBit(variableName, bit), out wire);
        }


        public static Wire GetWireForBit(char variableName, byte bit)
        {
            return Wires[GetWireNameForBit(variableName, bit)];
        }
        public static string GetWireNameForBit(char variableName, byte bit)
        {
            return $"{variableName}{((bit < 10) ? "0" : "")}{bit}";
        }

    }

}