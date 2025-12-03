using System.Collections;

namespace AdventOfCode.Year2024;

using Vector2i = (int X, int Y);

public partial record Day15
{

    public abstract class ObjectPosition : IEnumerable<Vector2i>
    {

        public abstract IEnumerator<Vector2i> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => $"[{string.Join(", ", this)}]";

        public override bool Equals(object? obj)
        {
            if (obj is not ObjectPosition position) return false;
            return ReferenceEquals(this, position) || this.SequenceEqual(position);
        }

        public override int GetHashCode()
        {
            var hashCode = this.Aggregate(
                new HashCode(),
                (result, current) =>
                {
                    result.Add(current);
                    return result;
                }
            );

            return hashCode.ToHashCode();
        }

    }

    public class ChestPosition(Vector2i left, Vector2i right) : ObjectPosition
    {

        public override IEnumerator<Vector2i> GetEnumerator()
        {
            yield return left;
            yield return right;
        }

    }

    public abstract class SinglePosition(Vector2i position) : ObjectPosition
    {

        public override IEnumerator<Vector2i> GetEnumerator()
        {
            yield return position;
        }

    }

    public class RobotPosition(Vector2i position) : SinglePosition(position);

    public class WallPosition(Vector2i position) : SinglePosition(position);

}