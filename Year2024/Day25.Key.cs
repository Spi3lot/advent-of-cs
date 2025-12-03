namespace AdventOfCode.Year2024;

public partial record Day25
{

    private sealed class Schematic
    {

        private readonly int[] _heights;

        private readonly int _space;

        public bool IsKey { get; }

        public Schematic(string[] schematic)
        {
            schematic = schematic.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            string firstLine = schematic[0];
            _heights = new int[firstLine.Length];
            _space = schematic.Length - 1;
            IsKey = firstLine[0] == '.';

            foreach (string line in schematic[1..])
            {
                foreach ((int index, char @char) in line.Index())
                {
                    if (@char == '#') _heights[index]++;
                }
            }
        }

        public bool Fits(Schematic other)
        {
            if (IsKey == other.IsKey) throw new ArgumentException("Comparing 2 keys or 2 locks with each other");
            if (_space != other._space) throw new ArgumentException($"{_space} != {other._space}");
            return _heights.Zip(other._heights).All(height => height.First + height.Second <= _space);
        }

    }

}