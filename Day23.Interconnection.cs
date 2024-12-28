
using System.Collections.Immutable;

namespace AdventOfCode;

public partial record Day23
{

    private sealed class Interconnection : HashSet<Computer>
    {

        public override bool Equals(object? obj)
        {
            if (obj is not Interconnection other) return false;
            return IsSubsetOf(other) && IsSupersetOf(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.Aggregate(19, (result, current) => result * 31 + current.GetHashCode());
            }
        }

        public override string ToString()
        {
            return string.Join(',', this.Select(computer => computer.Name));
        }

        public new IEnumerator<Computer> GetEnumerator()
        {
            return this.ToImmutableSortedSet().GetEnumerator();
        }

    }

}