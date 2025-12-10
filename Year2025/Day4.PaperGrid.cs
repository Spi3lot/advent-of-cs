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
            CalculateNeighborCounts();
        }

        public int[,] PaperNeighborCounts { get; }

        public int RowCount { get; }

        public int ColumnCount { get; }

        public IEnumerable<(int Y, int X)> AccessiblePaperRolls()
        {
            return PaperNeighborCounts
                .Cast<int>()
                .Index()
                .Where(neighborCount => neighborCount.Item is >= 0 and < 4)
                .Select(x => Math.DivRem(x.Index, ColumnCount));
        }

        public void InformPaperNeighborsOfAbsence(int i, int j)
        {
            _grid[j, i] = '.';
            PaperNeighborCounts[j, i] = -1;
            InformPaperNeighbors(i, j, Information.Absence);
        }

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

        private void CalculateNeighborCounts()
        {
            for (int j = 0; j < RowCount; j++)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (_grid[j, i] == '@')
                    {
                        InformPaperNeighbors(i, j, Information.Presence);
                    }
                    else
                    {
                        PaperNeighborCounts[j, i] = -1;
                    }
                }
            }
        }

        private void InformPaperNeighbors(int i, int j, Information info)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (y + j < 0 || y + j >= RowCount)
                {
                    continue;
                }

                for (int x = -1; x <= 1; x++)
                {
                    if ((x != 0 || y != 0)
                        && x + i >= 0
                        && x + i < ColumnCount
                        && _grid[j + y, i + x] == '@')
                    {
                        PaperNeighborCounts[j + y, i + x] += (int) info;
                    }
                }
            }
        }

        private enum Information
        {

            Absence = -1,

            Presence = 1,

        }

    }

}