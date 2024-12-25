
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
                int hash = 19;

                foreach (var computer in this.ToImmutableSortedSet())
                {
                    hash = hash * 31 + computer.GetHashCode();
                }

                return hash;
            }
        }

    }

}