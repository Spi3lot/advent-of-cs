namespace AdventOfCode.Year2025;

public partial record Day7
{

    public override void SolvePart2()
    {
        Console.WriteLine(new TachyonManifold(Input).CountTimelines());
    }

    public partial class TachyonManifold
    {

        private readonly Dictionary<(int X, int Y), ulong> _timelineCache = [];

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

    }

}