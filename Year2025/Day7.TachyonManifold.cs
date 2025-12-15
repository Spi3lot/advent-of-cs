namespace AdventOfCode.Year2025;

public partial record Day7
{

    public class TachyonManifold
    {

        private readonly string[] _grid;

        private readonly (int Y, int X) _startPosition;

        private readonly List<TachyonBeam> _beams = [];

        public TachyonManifold(string input)
        {
            _grid = input.Trim().Split('\n');
            Width = _grid[0].Length;
            Height = _grid.Length - 1;
            _startPosition = Math.DivRem(input.IndexOf('S'), Width);
        }

        public int Width { get; }

        public int Height { get; }

        public int SplitCount { get; private set; }

        public void Evaluate()
        {
            SplitCount = 0;
            _beams.Clear();
            _beams.Add(new TachyonBeam(this) { X = _startPosition.X, Y = _startPosition.Y, });

            while (_beams.Count > 0)
            {
                _beams.ToList().ForEach(beam => beam.Move());
            }
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
                manifold.SplitCount++;
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