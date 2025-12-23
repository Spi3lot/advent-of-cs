namespace AdventOfCode.Year2025;

public partial record Day10
{

    public class Button
    {

        public Button(IEnumerable<int> wiring)
        {
            Wiring = wiring.ToArray();
            Bitmask = Wiring.Aggregate(0, (mask, pin) => mask | (1 << pin));
        }

        public int[] Wiring { get; }

        public int Bitmask { get; }

        public static Button Parse(string input)
        {
            var wiring = input.Trim("()")
                .ToString()
                .Split(',')
                .Select(int.Parse);

            return new Button(wiring);
        }

    }

}