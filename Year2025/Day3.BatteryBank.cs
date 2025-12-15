namespace AdventOfCode.Year2025;

public partial record Day3
{

    public class BatteryBank(string input)
    {

        private readonly int[] _batteries = input.Select(battery => int.Parse(battery.ToString())).ToArray();

        public int FindMaximumJoltageFor2Batteries()
        {
            if (_batteries.Length < 2)
            {
                throw new InvalidOperationException($"Not enough batteries ({_batteries.Length} instead of at least 2)");
            }

            var sortedBatterySet = new SortedSet<int>(_batteries);
            int joltage = sortedBatterySet.Max;
            int maxIndex = Array.IndexOf(_batteries, joltage);

            if (maxIndex + 1 < _batteries.Length)
            {
                int rightMax = _batteries[(maxIndex + 1)..].Max();
                return 10 * joltage + rightMax;
            }

            int leftMax = _batteries[..maxIndex].Max();
            return 10 * leftMax + joltage;
        }

        public long FindMaximumJoltageForNBatteries(int n)
        {
            int startIndex = 0;
            long maxJoltage = 0;

            for (int i = n - 1; i >= 0; i--)
            {
                var max = _batteries[startIndex..^i]
                    .Index()
                    .MaxBy(x => x.Item);

                startIndex += 1 + max.Index;
                maxJoltage = 10 * maxJoltage + max.Item;
            }

            return maxJoltage;
        }

    }

}