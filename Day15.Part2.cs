using System;
using System.Text;

namespace AdventOfCode;

using static AdventOfCode.Day15;
using Vector2i = (int X, int Y);

public partial record Day15
{

    private readonly DoubleChestRobot _doubleChestRobot;

    public override void SolvePart2()
    {
        MoveRobot(_doubleChestRobot);
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

        private void Move(IObjectPosition position, Vector2i delta)
        {
            foreach (var obstruction in GetObstructions(position, delta))
            {
                Move(obstruction, delta);
            }

            foreach (var (X, Y) in position)
            {
                Vector2i moved = (X + delta.X, Y + delta.Y);
                (Grid[moved.Y, moved.X], Grid[Y, X]) = (Grid[Y, X], Grid[moved.Y, moved.X]);
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
                .Where(objectPosition => !objectPosition.SequenceEqual(position))
                .ToHashSet();
        }

        private IObjectPosition? GetObjectPosition(Vector2i position)
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