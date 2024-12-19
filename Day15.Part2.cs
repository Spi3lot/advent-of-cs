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
            /* ONLY FOR REFERENCE
            var vacant = (X: GpsCoordinates.X + delta.X, Y: GpsCoordinates.Y + delta.Y);
            char current;

            while ((current = Grid![vacant.Y, vacant.X]) != '.')
            {
                vacant.X += delta.X;
                vacant.Y += delta.Y;
                if (current == '#') return false;
            }

            // If the robot did not have to push any chests,
            // the O will get overwritten by an @ anyway
            Grid[vacant.Y, vacant.X] = 'O';
            Grid[GpsCoordinates.Y, GpsCoordinates.X] = '.';
            GpsCoordinates.X += delta.X;
            GpsCoordinates.Y += delta.Y;
            Grid[GpsCoordinates.Y, GpsCoordinates.X] = '@';
            */
            return true;
        }

        // TODO: merge with CanChestMove?
        private bool CanMove(Vector2i position, Vector2i delta)
        {
            Vector2i offset = (position.X + delta.X, position.Y + delta.Y);
            char cell = Grid![offset.Y, offset.X];

            return cell switch
            {
                '[' => CanChestMove((offset, (offset.X + 1, offset.Y)), delta),
                ']' => CanChestMove(((offset.X - 1, offset.Y), offset), delta),
                '.' => true,
                _ => false,
            };
        }

        private bool CanChestMove((Vector2i Left, Vector2i Right) position, Vector2i delta)
        {
            (Vector2i Left, Vector2i Right) offset = (
                (position.Left.X + delta.X, position.Left.Y + delta.Y),
                (position.Right.X + delta.X, position.Right.Y + delta.Y)
            );

            if (true) return false;
        }

    }

}