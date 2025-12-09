namespace AdventOfCode.Year2025;

public partial record Day4
{

    public class PaperGrid
    {

        private readonly char[,] _grid;

        public PaperGrid(string input)
        {
            string[] lines = input.Trim().Split('\n');
            RowCount = lines.Length;
            ColumnCount = (RowCount == 0) ? 0 : lines[0].Length;
            PaperNeighborCounts = new int[RowCount, ColumnCount];
            _grid = new char[RowCount, ColumnCount];
            FillGrid(lines);
            RecalculateNeighborCounts();
        }

        public int[,] PaperNeighborCounts { get; }

        public int RowCount { get; }

        public int ColumnCount { get; }

        private void FillGrid(string[] lines)
        {
            for (int j = 0; j < RowCount; j++)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    _grid[j, i] = lines[j][i];
                }
            }
        }

        private void RecalculateNeighborCounts()
        {
            for (int j = 0; j < RowCount; j++)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (_grid[j, i] == '@')
                    {
                        RecalculateNeighborCounts(i, j);
                    }
                }
            }
        }

        private void RecalculateNeighborCounts(int i, int j)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if ((x != 0 || y != 0)
                        && x + i >= 0
                        && x + i < ColumnCount
                        && y + j >= 0
                        && y + j < RowCount
                        && _grid[j + y, i + x] == '@')
                    {
                        PaperNeighborCounts[j + y, i + x]++;
                    }
                }
            }
        }

    }

}