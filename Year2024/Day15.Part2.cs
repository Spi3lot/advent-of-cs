namespace AdventOfCode.Year2024;

using Vector2i = (int X, int Y);

public partial record Day15
{

    private readonly Robot _doubleChestRobot;

    public override void SolvePart2()
    {
        ApplyMovements(_doubleChestRobot);
    }

    public class DoubleChestRobot(char[,] grid) : Robot(grid)
    {

        protected override bool Move(Vector2i delta)
        {
            var position = GetObjectPosition(GpsCoordinates)!;
            if (!CanMove(position, delta)) return false;
            Move(position, delta);
            (GpsCoordinates.X, GpsCoordinates.Y) = (GpsCoordinates.X + delta.X, GpsCoordinates.Y + delta.Y);
            return true;
        }

        private void Move(ObjectPosition position, Vector2i delta)
        {
            foreach (var obstruction in GetObstructions(position, delta))
            {
                Move(obstruction, delta);
            }

            char[] chars = new char[position.Count()];

            foreach (var (index, (x, y)) in position.Index())
            {
                chars[index] = Grid[y, x];
                Grid[y, x] = '.';
            }

            foreach (var (index, (x, y)) in position.Index())
            {
                Grid[y + delta.Y, x + delta.X] = chars[index];
            }
        }

        private bool CanMove(ObjectPosition position, Vector2i delta)
        {
            return GetObstructions(position, delta)
                .All(obstruction => obstruction is ChestPosition && CanMove(obstruction, delta));
        }

        private HashSet<ObjectPosition> GetObstructions(ObjectPosition position, Vector2i delta)
        {
            return position
                .Select(value => (value.X + delta.X, value.Y + delta.Y))
                .Select(GetObjectPosition)
                .Where(objectPosition => objectPosition != null)
                .Select(objectPosition => objectPosition!)
                .Where(objectPosition => !objectPosition.Equals(position))
                .ToHashSet();
        }

        private ObjectPosition? GetObjectPosition(Vector2i position)
        {
            return Grid[position.Y, position.X] switch
            {
                '[' => new ChestPosition(position, (position.X + 1, position.Y)),
                ']' => new ChestPosition((position.X - 1, position.Y), position),
                '#' => new WallPosition(position),
                '@' => new RobotPosition(position),
                '.' => null,
                _ => throw new ArgumentException(
                    $"Invalid object {Grid[position.Y, position.X]} at ${position}",
                    nameof(position)
                )
            };
        }

    }

}