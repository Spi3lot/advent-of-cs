namespace AdventOfCode.Year2025;

public partial record Day7
{

    public class Teleporter
    {

        private readonly List<Beam> _beams = [];

        public int SplitCount { get; private set; }

        public class Beam(Teleporter teleporter)
        {

            public int X { get; private set; }

            public int Y { get; private set; }

            public void Split()
            {
                teleporter.SplitCount++;
                teleporter._beams.Add(new Beam(teleporter) { X = X, Y = Y });
                int duplicateCount = teleporter._beams.Count - teleporter._beams.DistinctBy(beam => (beam.X, beam.Y)).Count();

                if (duplicateCount > 0)
                {
                    Console.WriteLine(duplicateCount);
                }
            }

        }

    }

}