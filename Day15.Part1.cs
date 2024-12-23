namespace AdventOfCode;

public partial record Day15
{

    private readonly SingleChestRobot _singleChestRobot;

    public override void SolvePart1()
    {
        MoveRobot(_singleChestRobot);
    }

    public class SingleChestRobot(char[,] grid) : Robot(grid)
    {

        protected override bool Move((int X, int Y) delta)
        {
            var vacant = (X: GpsCoordinates.X + delta.X, Y: GpsCoordinates.Y + delta.Y);
            char current;

            while ((current = Grid[vacant.Y, vacant.X]) != '.')
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
            return true;
        }

    }

}