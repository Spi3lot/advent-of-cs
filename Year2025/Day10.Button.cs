namespace AdventOfCode.Year2025;

public partial record Day10
{

    public class Button
    {

        public int Bitmask { get; private init; }

        public static Button Parse(string input)
        {
            var wiring = input.Trim("()")
                .ToString()
                .Split(',')
                .Select(int.Parse);
            
            return FromWiring(wiring);
        }

        public static Button FromWiring(IEnumerable<int> wiring)
        {
            return new Button { Bitmask = wiring.Aggregate(0, (mask, pin) => mask | 1 << pin) };
        }

    }

}