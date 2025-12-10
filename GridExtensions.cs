using System.Text;

namespace AdventOfCode;

public static class GridExtensions
{

    extension<T>(T[,] grid)
    {

        public void Print(string columnSeparator = "", string rowSeparator = "\n")
        {
            var stringBuilder = new StringBuilder(columnSeparator.Length * grid.Length
                                                  + rowSeparator.Length * grid.GetLength(0));
            
            for (int j = 0; j < grid.GetLength(0); ++j)
            {
                for (int i = 0; i < grid.GetLength(1); ++i)
                {
                    stringBuilder.Append(grid[j, i]).Append(columnSeparator);
                }

                stringBuilder.Append(rowSeparator);
            }

            Console.Write(stringBuilder);
        }

        public void ForEachCell(Action<T, int, int> action)
        {
            for (int j = 0; j < grid.GetLength(0); ++j)
            {
                for (int i = 0; i < grid.GetLength(1); ++i)
                {
                    action(grid[j, i], i, j);
                }
            }
        }

        public void ForEachCell(Action<T> cellAction, Action rowAction)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                for (int i = 0; i < grid.GetLength(1); i++)
                {
                    cellAction(grid[j, i]);
                }

                rowAction();
            }
        }

    }

    public static T[,] To2D<T>(this T[][] source)
    {
        try
        {
            int dim0 = source.Length;
            int dim1 = source.GroupBy(row => row.Length).Single()
                .Key; // throws InvalidOperationException if source is not rectangular
            var result = new T[dim0, dim1];

            for (int j = 0; j < dim0; j++)
            {
                for (int i = 0; i < dim1; i++)
                {
                    result[j, i] = source[j][i];
                }
            }

            return result;
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("The given jagged array is not rectangular.");
        }
    }

}