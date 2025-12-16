namespace AdventOfCode.Year2025;

public partial record Day7() : AdventDay<Day7>(2025)
{

    public partial class TachyonManifold
    {

        private readonly string[] _grid;

        private readonly (int Y, int X) _startPosition;

        public TachyonManifold(string input)
        {
            _grid = input.Trim().Split('\n');
            Width = _grid[0].Length;
            Height = _grid.Length - 1;
            _startPosition = Math.DivRem(input.IndexOf('S'), Width);
        }

        public int Width { get; }

        public int Height { get; }

    }

}