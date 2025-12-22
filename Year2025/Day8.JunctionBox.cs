namespace AdventOfCode.Year2025;

public partial record Day8
{

    public class JunctionBox(int x, int y, int z)
    {

        public int X { get; } = x;

        public int Y { get; } = y;

        public int Z { get; } = z;

        public static JunctionBox FromArray(int[] xyz)
        {
            return new JunctionBox(xyz[0], xyz[1], xyz[2]);
        }

        public double DistanceTo(JunctionBox to)
        {
            double xx = to.X - X;
            double yy = to.Y - Y;
            double zz = to.Z - Z;
            return Math.Sqrt(xx * xx + yy * yy + zz * zz);
        }

    }

}