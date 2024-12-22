namespace AdventOfCode;

using Vector2i = (int X, int Y);

public partial record Day15
{

    private readonly DoubleChestRobot _doubleChestRobot = new();

    public override void SolvePart2()
    {
        SolvePart(_doubleChestRobot);
    }

    public class DoubleChestRobot : Robot
    {

        protected override bool Move(Vector2i delta)
        {
            var position = GetObjectPosition(GpsCoordinates)!;
            if (!CanMove(position, delta)) return false;
            Move(position, delta);
            return true;
        }

        private void Move(IObjectPosition position, Vector2i delta)
        {
            foreach (var obstruction in GetObstructions(position, delta))
            {
                Move(obstruction, delta);
            }

            foreach (var value in position)
            {
                Vector2i moved = (value.Y + delta.Y, value.X + delta.X);
                (Grid![moved.Y,moved.X], Grid[value.Y, value.X]) = (Grid[value.Y, value.X], Grid[moved.Y, moved.X]);
            }
        }

        private bool CanMove(IObjectPosition position, Vector2i delta)
        {
            return GetObstructions(position, delta)
                .All(obstruction => obstruction is ChestPosition chest && CanMove(chest, delta));
        }

        private HashSet<IObjectPosition> GetObstructions(IObjectPosition position, Vector2i delta)
        {
            return position
                .Select(value => (value.X + delta.X, value.Y + delta.Y))
                .Select(GetObjectPosition)
                .Where(objectPosition => objectPosition != null)
                .Select(objectPosition => objectPosition!)
                .Where(objectPosition => objectPosition != position)
                .ToHashSet();
        }

        private IObjectPosition? GetObjectPosition(Vector2i position)
        {
            return Grid![position.Y, position.X] switch
            {
                '[' => new ChestPosition(position, (position.X + 1, position.Y)),
                ']' => new ChestPosition((position.X - 1, position.Y), position),
                '#' => new WallPosition(position),
                '@' => new RobotPosition(position),
                '.' => null,
                _ => throw new ArgumentException(
                    $"Invalid object {Grid![position.Y, position.X]} at ${position}",
                    nameof(position)
                )
            };
        }

    }

}