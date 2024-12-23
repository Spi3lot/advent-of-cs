using System.Collections;

namespace AdventOfCode;

using Vector2i = (int X, int Y);

public partial record Day15
{

    public abstract class IObjectPosition : IEnumerable<Vector2i>
    {

        public abstract IEnumerator<(int X, int Y)> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class ChestPosition(Vector2i left, Vector2i right) : IObjectPosition
    {

        public Vector2i Left { get; } = left;

        public Vector2i Right { get; } = right;

        public override IEnumerator<Vector2i> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

    }

    public abstract class SinglePosition(Vector2i position) : IObjectPosition
    {

        public Vector2i Value { get; } = position;

        public override IEnumerator<Vector2i> GetEnumerator()
        {
            yield return Value;
        }

    }

    public class RobotPosition(Vector2i position) : SinglePosition(position);

    public class WallPosition(Vector2i position) : SinglePosition(position);

}