using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode.Year2025;

public partial record Day10
{

    public class Lights
    {

        public int Bitmask { get; private init; }

        public static Lights Parse(string input)
        {
            string binaryLights = input.Trim("[]")
                .ToString()
                .Replace('.', '0')
                .Replace('#', '1');

            string reversedBits = new(binaryLights.Reverse().ToArray());
            return new Lights { Bitmask = int.Parse(reversedBits, NumberStyles.BinaryNumber) };
        }
        
    }

}