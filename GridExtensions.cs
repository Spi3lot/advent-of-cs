using System.Text;

namespace AdventOfCode;

public static class GridExtensions
{

    public static void Print<T>(this T[,] grid)
    {
        var stringBuilder = new StringBuilder(grid.Length + grid.GetLength(0) * Environment.NewLine.Length);

        for (int j = 0; j < grid.GetLength(0); ++j)
        {
            for (int i = 0; i < grid.GetLength(1); ++i)
            {
                stringBuilder.Append(grid[j, i]);
            }

            stringBuilder.AppendLine();
        }

        Console.Write(stringBuilder);
    }

    public static void ForEachCell<T>(this T[,] grid, Action<T, int, int> action)
    {
        for (int j = 0; j < grid.GetLength(0); ++j)
        {
            for (int i = 0; i < grid.GetLength(1); ++i)
            {
                action(grid[j, i], i, j);
            }
        }
    }

    public static void ForEachCell<T>(this T[,] grid, Action<T> cellAction, Action rowAction)
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

    public static T[,] To2D<T>(this T[][] source)
    {
        try
        {
            int dim0 = source.Length;
            int dim1 = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular
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