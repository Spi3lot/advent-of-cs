namespace AdventOfCode;

public partial record Day15
{

    public abstract class RobotBase
    {

        public char[,]? Grid;

        public (int X, int Y) GpsCoordinates;
        protected abstract bool Move((int X, int Y) delta);

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

        public long SumBoxGpsCoordinates()
        {
            long sum = 0;
            Grid!.ForEachCell((cell, i, j) => sum += (cell == 'O') ? 100 * j + i : 0);
            return sum;
        }

    }

}