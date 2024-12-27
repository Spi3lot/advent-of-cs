
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
                return this
                    .ToImmutableSortedSet()
                    .Aggregate(19, (result, current) => result * 31 + current.GetHashCode());
            }
        }

    }

}