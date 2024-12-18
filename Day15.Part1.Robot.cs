using System.Text;

namespace AdventOfCode;

public partial record Day15 : AdventDay<Day15>
{

    private static class Part1
    {
        
        public static readonly Robot Bot = new();

        public class Robot
        {

            public char[,]? Grid { get; set; }

            public (int X, int Y) GpsCoordinates;

            public bool Move(char movement)
            {
                return movement switch
                {
                    '>' => Move((1, 0)),
                    'v' => Move((0, 1)),
                    '<' => Move((-1, 0)),
                    '^' => Move((0, -1)),
                    _ => false
                };
            }

            private bool Move((int X, int Y) delta)
            {
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
                return true;
            }

            public long SumBoxGpsCoordinates()
            {
                long sum = 0;
                char[,] grid = Grid!;

                for (long j = 0; j < grid.GetLength(0); j++)
                {
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        if (grid[j, i] == 'O') sum += 100 * j + i;
                    }
                }

                return sum;
            }

        }

    }

}