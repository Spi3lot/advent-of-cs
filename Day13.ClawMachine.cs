namespace AdventOfCode;

using Buttons = ((int X, int Y, int Cost) A, (int X, int Y, int Cost) B);

public partial record Day13
{

    private sealed class ClawMachine(Buttons buttons, (int, int) prizeLocation)
    {

        private static readonly StringSplitOptions _stringSplitOptions = StringSplitOptions.RemoveEmptyEntries |
                                                                         StringSplitOptions.TrimEntries;

        public Buttons Buttons { get; } = buttons;

        public (int X, int Y) PrizeLocation { get; } = prizeLocation;

        public static ClawMachine Parse(string lines)
        {
            var coordinates = lines.Split('\n', _stringSplitOptions)
                .Select(line => line.Split(": ", _stringSplitOptions)[1])
                .Select(rightHandSide => rightHandSide.Split(", ", _stringSplitOptions)
                    .Select(xy => xy.Split(['+', '='], _stringSplitOptions))
                    .Select(xy => int.Parse(xy[1]))
                    .ToArray()
                )
                .Select(xy => (X: xy[0], Y: xy[1]))
                .ToArray();

            var buttons = new Buttons
            {
                A = (coordinates[0].X, coordinates[0].Y, 3),
                B = (coordinates[1].X, coordinates[1].Y, 1)
            };

            var prizeLocation = (coordinates[2].X, coordinates[2].Y);
            return new ClawMachine(buttons, prizeLocation);
        }

    }

}