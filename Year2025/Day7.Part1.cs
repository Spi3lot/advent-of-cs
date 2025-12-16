namespace AdventOfCode.Year2025;

public partial record Day7
{

    public override void SolvePart1()
    {
        Console.WriteLine(new TachyonManifold(Input).CountSplits());
    }

    public partial class TachyonManifold
    {

        private readonly List<TachyonBeam> _beams = [];

        private ulong _splitCount;

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