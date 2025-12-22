namespace AdventOfCode.Year2025;

public partial record Day10
{

    public static class Button
    {

        public static int GetBitmask(string input)
        {
            var wiring = input.Trim("()")
                .ToString()
                .Split(',')
                .Select(int.Parse);
            
            return FromWiring(wiring);
        }

        public static int FromWiring(IEnumerable<int> wiring)
        {
            return wiring.Aggregate(0, (mask, pin) => mask | 1 << pin);
        }

    }

}