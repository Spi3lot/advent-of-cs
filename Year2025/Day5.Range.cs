namespace AdventOfCode.Year2025;

public partial record Day5
{

    public class Range
    {

        public required long Start { get; set; }

        public required long End { get; set; }

        public long Count => End - Start + 1;
        
        public static Range FromString(string input) => FromArray(input.Split('-').Select(long.Parse).ToArray());

        public static Range FromArray(long[] array)
        {
            return (array.Length == 2)
                ? new Range { Start = array[0], End = array[1] }
                : throw new ArgumentException("Must provide an array of size 2");
        }

        public bool Merge(Range range)
        {
            var minStartRange = (Start < range.Start) ? this : range;
            var maxStartRange = (Start < range.Start) ? range : this;
            var maxEndRange = (End < range.End) ? range : this;

            if (minStartRange.End + 1 < maxStartRange.Start)
            {
                return false;
            }

            Start = minStartRange.Start;
            End = maxEndRange.End;
            return true;
        }

        public override string ToString() => $"{Start}-{End}";

    }

}