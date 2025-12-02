namespace AdventOfCode.Year2024;

using ButtonPressCount = (long Value, long Numerator, long Denominator);
using Buttons = ((long X, long Y, long Cost) A, (long X, long Y, long Cost) B);

public partial record Day13
{

    private sealed class ClawMachine(Buttons buttons, (long, long) prize)
    {

        private const StringSplitOptions SplitOptions = StringSplitOptions.RemoveEmptyEntries |
                                                              StringSplitOptions.TrimEntries;

        private Buttons Buttons { get; } = buttons;

        public (long X, long Y) Prize = prize;

        public static ClawMachine Parse(string lines)
        {
            var coordinates = lines.Split('\n', SplitOptions)
                .Select(line => line.Split(": ", SplitOptions)[1])
                .Select(rightHandSide => rightHandSide.Split(", ", SplitOptions)
                    .Select(xy => xy.Split(['+', '='], SplitOptions))
                    .Select(xy => long.Parse(xy[1]))
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

        public long CalcMinimumTokenCountForPrize()
        {
            checked
            {
                var (A, B) = (new ButtonPressCount(), new ButtonPressCount());
                B.Numerator = Buttons.A.Y * Prize.X - Buttons.A.X * Prize.Y;
                B.Denominator = Buttons.A.Y * Buttons.B.X - Buttons.A.X * Buttons.B.Y;
                if (B.Numerator % B.Denominator != 0) return 0;
                B.Value = B.Numerator / B.Denominator;
                A.Numerator = Prize.X - Buttons.B.X * B.Value;
                A.Denominator = Buttons.A.X;
                if (A.Numerator % A.Denominator != 0) return 0;
                A.Value = A.Numerator / A.Denominator;
                return Buttons.A.Cost * A.Value + Buttons.B.Cost * B.Value;
            }
        }

    }

}