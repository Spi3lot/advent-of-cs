using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode;

public partial record Day24
{

    private sealed class ConstantWire(bool value) : Wire, IParsable<ConstantWire>
    {

        public override bool Value => value;

        public override int Depth => 0;

        public static ConstantWire Parse(string s, IFormatProvider? provider) => new(s[0] == '1');

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ConstantWire result)
        {
            try
            {
                result = Parse(s!, provider);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

    }

}