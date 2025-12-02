using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace AdventOfCode.Year2024;

public partial record Day14
{

    private sealed class Robot(Vector128<long> position, Vector128<long> velocity)
    {

        public Vector128<long> Position { get; private set; } = position;

        public Vector128<long> Velocity { get; } = velocity;


        public static Robot Parse(string line)
        {
            var vectors = line.Split(' ')
                .Select(equation => equation.Split('=')[1])
                .Select(coordinates => coordinates.Split(',').Select(long.Parse))
                .Select(coordinates => Vector128.Create([..coordinates]))
                .ToArray();

            return new Robot(vectors[0], vectors[1]);
        }

        public void MoveForOneSecond()
        {
            Position = Vector128.Add(Position, Velocity);
        }

        public void Move(long seconds)
        {
            Position = Vector128.Add(Position, Vector128.Multiply(Velocity, seconds));
        }

        public void Clamp(Vector128<long> gridDimensions)
        {
            Position = Vector128.Create(Mod(Position[0], gridDimensions[0]), Mod(Position[1], gridDimensions[1]));
        }

        private static long Mod(long a, long b)
        {
            return (a % b + b) % b;
        }

    }

}