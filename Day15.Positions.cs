using System.Collections;

namespace AdventOfCode;

using Vector2i = (int X, int Y);

public partial record Day15
{

    public interface IObjectPosition : IEnumerable<Vector2i>;

    public class ChestPosition(Vector2i left, Vector2i right) : IObjectPosition
    {

        public Vector2i Left { get; } = left;

        public Vector2i Right { get; } = right;

        public IEnumerator<Vector2i> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
    
    public abstract class SinglePosition(Vector2i position) : IObjectPosition
    {

        public Vector2i Value { get; } = position;

        public IEnumerator<Vector2i> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
    
    public class RobotPosition(Vector2i position) : SinglePosition(position);
    
    public class WallPosition(Vector2i position) : SinglePosition(position);

}