using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode.Year2025;

public partial record Day10
{

    public static class Lights
    {

        public static int GetBitmask(string input)
        {
            string binaryLights = input.Trim("[]")
                .ToString()
                .Replace('.', '0')
                .Replace('#', '1');

            string reversedBits = new(binaryLights.Reverse().ToArray());
            return int.Parse(reversedBits, NumberStyles.BinaryNumber);
        }
        
    }

}