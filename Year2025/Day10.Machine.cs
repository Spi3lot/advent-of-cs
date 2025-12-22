using System.Numerics;

namespace AdventOfCode.Year2025;

public partial record Day10
{

    public class Machine
    {

        public int LightBitmask { get; }

        public int[] ButtonBitmasks { get; }

        public int[] Joltages { get; }

        public Machine(string input)
        {
            string[] parts = input.Split(' ');
            LightBitmask = Lights.GetBitmask(parts[0]);
            ButtonBitmasks = parts[1..^1].Select(Button.GetBitmask).ToArray();

            Joltages = parts[^1].Trim("{}")
                .ToString()
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }


        public int DetermineMinimumButtonPresses()
        {
            return GetValidButtonPressMasks().Min(BitOperations.PopCount);
        }

        public IEnumerable<uint> GetValidButtonPressMasks()
        {
            for (uint mask = 0; mask < (1u << ButtonBitmasks.Length); mask++)
            {
                int lights = ButtonBitmasks.Where((_, index) => (mask & (1u << index)) != 0)
                    .Aggregate(0, (result, buttonBitmask) => result ^ buttonBitmask);

                if (lights == LightBitmask)
                {
                    yield return mask;
                }
            }
        }

    }

}