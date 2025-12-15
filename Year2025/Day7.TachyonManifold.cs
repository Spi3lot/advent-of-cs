namespace AdventOfCode.Year2025;

public partial record Day7
{

    public class TachyonManifold
    {

        private readonly string[] _grid;

        private readonly (int Y, int X) _startPosition;

        private readonly List<TachyonBeam> _beams = [];

        private readonly Dictionary<(int X, int Y), ulong> _timelineCache = [];

        private ulong _splitCount;

        public TachyonManifold(string input)
        {
            _grid = input.Trim().Split('\n');
            Width = _grid[0].Length;
            Height = _grid.Length - 1;
            _startPosition = Math.DivRem(input.IndexOf('S'), Width);
        }

        public int Width { get; }

        public int Height { get; }

        public ulong CountSplits()
        {
            _splitCount = 0;
            _beams.Clear();
            _beams.Add(new TachyonBeam(this) { X = _startPosition.X, Y = _startPosition.Y, });

            while (_beams.Count > 0)
            {
                _beams.ToList().ForEach(beam => beam.Move());
            }

            return _splitCount;
        }

        public ulong CountTimelines()
        {
            return CountTimelines(_startPosition.X, _startPosition.Y);
        }

        private ulong CountTimelines(int x, int y)
        {
            if (_timelineCache.TryGetValue((x, y), out ulong result))
            {
                return result;
            }

            if (y == Height)
            {
                return 1;
            }

            ulong value = (_grid[y][x] == '^')
                ? CountTimelines(x - 1, y) + CountTimelines(x + 1, y)
                : CountTimelines(x, y + 1);

            _timelineCache[(x, y)] = value;
            return value;
        }

        public class TachyonBeam(TachyonManifold manifold)
        {

            public int X { get; set; }

            public int Y { get; set; }

            public void Move()
            {
                if (++Y == manifold.Height)
                {
                    manifold._beams.Remove(this);
                    return;
                }

                SplitIfEncounteredSplitter();
            }

            private void SplitIfEncounteredSplitter()
            {
                if (manifold._grid[Y][X] != '^')
                {
                    return;
                }

                manifold._beams.Add(new TachyonBeam(manifold) { X = X - 1, Y = Y });
                manifold._splitCount++;
                X++;
                RemoveDuplicates();
            }

            private void RemoveDuplicates()
            {
                var distinctBeams = manifold._beams
                    .DistinctBy(beam => (beam.X, beam.Y))
                    .ToList();

                manifold._beams.Clear();
                manifold._beams.AddRange(distinctBeams);
            }

        }

    }

}