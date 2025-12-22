using System.Numerics;

namespace AdventOfCode.Year2025;

public partial record Day10
{

    public class Machine
    {

        public Lights Lights { get; }

        public Button[] Buttons { get; }

        public int[][] Joltages { get; }

        public Machine(string input)
        {
            string[] parts = input.Split(' ');
            Lights = Lights.Parse(parts[0]);

            Buttons = parts[1..]
                .TakeWhile(x => x is ['(', .., ')'])
                .Select(Button.Parse)
                .ToArray();

            Joltages = parts[1..]
                .SkipWhile(x => x is not ['{', .., '}'])
                .Select(joltageArray => joltageArray.Trim("{}")
                    .ToString()
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray()
                )
                .ToArray();
        }


        public int DetermineMinimumButtonPresses()
        {
            return GetValidButtonPressMasks().Min(BitOperations.PopCount);
        }

        public IEnumerable<uint> GetValidButtonPressMasks()
        {
            for (uint mask = 0; mask < (1u << Buttons.Length); mask++)
            {
                int lights = Buttons.Where((_, index) => (mask & (1u << index)) != 0)
                    .Aggregate(0, (result, button) => result ^ button.Bitmask);

                if (lights == Lights.Bitmask)
                {
                    yield return mask;
                }
            }
        }

    }

}