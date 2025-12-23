using System.Numerics;

namespace AdventOfCode.Year2025;

public partial record Day10
{

    public class Machine
    {

        public Machine(string input)
        {
            string[] parts = input.Split(' ');
            LightBitmask = Lights.GetBitmask(parts[0]);
            Buttons = parts[1..^1].Select(Button.Parse).ToArray();

            Joltages = parts[^1].Trim("{}")
                .ToString()
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }

        public int LightBitmask { get; }

        public Button[] Buttons { get; }

        public int[] Joltages { get; }


        public int DetermineMinimumButtonPressCount(bool joltage)
        {
            return (joltage)
                ? GetValidJoltageButtonPressCounts().Min(Enumerable.Sum)
                : GetValidLightButtonPressMasks().Min(BitOperations.PopCount);
        }

        public IEnumerable<uint> GetValidLightButtonPressMasks()
        {
            for (int mask = 0; mask != 1 << Buttons.Length; mask++)
            {
                int lights = Buttons.Where((_, index) => (mask & (1 << index)) != 0)
                    .Aggregate(0, (result, button) => result ^ button.Bitmask);

                if (lights == LightBitmask)
                {
                    yield return (uint) mask;
                }
            }
        }

        public IEnumerable<int[]> GetValidJoltageButtonPressCounts()
        {
            return GetValidJoltageButtonPressCounts(
                0,
                new int[Buttons.Length],
                new int[Joltages.Length]
            );
        }

        private IEnumerable<int[]> GetValidJoltageButtonPressCounts(
            int buttonIndex,
            int[] pressCounts,
            int[] joltages
        )
        {
            if (buttonIndex >= Buttons.Length)
            {
                yield break;
            }

            do
            {
                var deeperPressCounts = GetValidJoltageButtonPressCounts(
                    buttonIndex + 1,
                    [..pressCounts],
                    [..joltages]
                );

                foreach (int[] counts in deeperPressCounts)
                {
                    yield return counts;
                }
            } while (TryPressJoltageButton(buttonIndex, pressCounts, joltages));

            if (joltages.SequenceEqual(Joltages))
            {
                yield return pressCounts;
            }
        }

        private bool TryPressJoltageButton(int buttonIndex, int[] pressCounts, int[] joltages)
        {
            if (!Buttons[buttonIndex].Wiring.All(pin => joltages[pin] + 1 <= Joltages[pin]))
            {
                return false;
            }

            foreach (int pin in Buttons[buttonIndex].Wiring)
            {
                joltages[pin]++;
            }

            pressCounts[buttonIndex]++;
            return true;
        }

        public IEnumerable<int> GetMaxJoltageButtonPressCounts()
        {
            return Buttons.Select(button => button.Wiring.Min(pin => Joltages[pin]));
        }

    }

}