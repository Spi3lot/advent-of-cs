using System.Numerics;

namespace AdventOfCode;

using Buttons = ((long X, long Y, long Cost) A, (long X, long Y, long Cost) B);
using ButtonPressCount = (long Value, long Numerator, long Denominator);

public partial record Day13
{

    private sealed class ClawMachine(Buttons buttons, (long, long) prize)
    {

        private static readonly StringSplitOptions StringSplitOptions = StringSplitOptions.RemoveEmptyEntries |
                                                                        StringSplitOptions.TrimEntries;

        private Buttons Buttons { get; } = buttons;

        public (long X, long Y) Prize = prize;

        public static ClawMachine Parse(string lines)
        {
            var coordinates = lines.Split('\n', StringSplitOptions)
                .Select(line => line.Split(": ", StringSplitOptions)[1])
                .Select(rightHandSide => rightHandSide.Split(", ", StringSplitOptions)
                    .Select(xy => xy.Split(['+', '='], StringSplitOptions))
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
                var pressCounts = (A: new ButtonPressCount(), B: new ButtonPressCount());
                pressCounts.B.Numerator = Buttons.A.Y * Prize.X - Buttons.A.X * Prize.Y;
                pressCounts.B.Denominator = Buttons.A.Y * Buttons.B.X - Buttons.A.X * Buttons.B.Y;
                if (pressCounts.B.Numerator % pressCounts.B.Denominator != 0) return 0;
                pressCounts.B.Value = pressCounts.B.Numerator / pressCounts.B.Denominator;
                pressCounts.A.Numerator = Prize.X - Buttons.B.X * pressCounts.B.Value;
                pressCounts.A.Denominator = Buttons.A.X;
                if (pressCounts.A.Numerator % pressCounts.A.Denominator != 0) return 0;
                pressCounts.A.Value = pressCounts.A.Numerator / pressCounts.A.Denominator;
                return Buttons.A.Cost * pressCounts.A.Value + Buttons.B.Cost * pressCounts.B.Value;
            }
        }

    }

}