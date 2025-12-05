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

        public int FindMaximumJoltageForNBatteriesBroken(int n)
        {
            var sortedBatterySet = new SortedSet<int>(_batteries);
            var batteryList = _batteries.ToList();

            if (batteryList.Count < n)
            {
                throw new InvalidOperationException($"Not enough batteries ({batteryList.Count} instead of at least {n})");
            }

            while (batteryList.Count >= n + batteryList.Count(battery => battery == sortedBatterySet.Min))
            {
                batteryList.RemoveAll(battery => battery == sortedBatterySet.Min);
                sortedBatterySet.Remove(sortedBatterySet.Min);
            }

            return batteryList.Count >= n
                ? batteryList.Take(n).Aggregate((result, next) => 10 * result + next)
                : throw new InvalidOperationException($"Okay well now we removed too many batteries ({batteryList.Count} instead of at least {n})");
        }

    }

}