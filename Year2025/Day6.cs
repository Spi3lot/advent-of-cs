using System.Text.RegularExpressions;

namespace AdventOfCode.Year2025;

public partial record Day6() : AdventDay<Day6>(2025)
{

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhiteSpaceRegex { get; }

    private static readonly Dictionary<string, Func<long, long, long>> OperatorFunctions = new()
    {
        ["+"] = (a, b) => a + b,
        ["*"] = (a, b) => a * b,
    };

    public override void SolvePart1()
    {
        string[][] rows = Input.Trim()
            .Split('\n')
            .Select(line => WhiteSpaceRegex.Split(line.Trim()))
            .ToArray();

        long[][] columns = Enumerable.Range(0, rows[0].Length)
            .Select(i => Enumerable.Range(0, rows.Length - 1)
                .Select(j => rows[j][i])
                .Select(long.Parse)
                .ToArray()
            )
            .ToArray();

        long grandTotal = rows[^1]
            .Select(op => OperatorFunctions[op])
            .Index()
            .Sum(op => columns[op.Index].Aggregate(op.Item));

        Console.WriteLine(grandTotal);
    }


    public override void SolvePart2()
    {
        string[] rawRows = Input.Trim().Split('\n');

        string[][] rows = rawRows
            .Select(line => WhiteSpaceRegex.Split(line.Trim()))
            .ToArray();

        string[][] columns = Enumerable.Range(0, rows[0].Length)
            .Select(i => Enumerable.Range(0, rows.Length - 1)
                .Select(j => rows[j][i])
                .ToArray()
            )
            .ToArray();

        int[] columnWidths = columns
            .Select(column => column.Max(num => num.Length))
            .ToArray();

        RestoreWhiteSpaces(rawRows, columns, columnWidths);

        long[][] transposed = columns.Index()
            .Select(column => (Width: columnWidths[column.Index], column.Item))
            .Select(column => Enumerable.Range(0, column.Width)
                .Select(i => column.Item.Aggregate(string.Empty, (result, item) => result + item[i]))
                .Select(long.Parse)
                .ToArray()
            )
            .ToArray();

        long grandTotal = rows[^1]
            .Select(op => OperatorFunctions[op])
            .Index()
            .Sum(op => transposed[op.Index].Aggregate(op.Item));

        Console.WriteLine(grandTotal);
    }

    private static void RestoreWhiteSpaces(string[] rawRows, string[][] columns, int[] columnWidths)
    {
        for (int j = 0; j < rawRows.Length - 1; j++)
        {
            RestoreRowWhiteSpaces(rawRows, columns, columnWidths, j);
        }
    }

    private static void RestoreRowWhiteSpaces(string[] rawRows, string[][] columns, int[] columnWidths, int rowIndex)
    {
        int outerColumnIndex = 0;
        int innerColumnIndex = 0;
        bool hitNumber = false;

        for (int i = 0; i < rawRows[rowIndex].Length; i++)
        {
            if (innerColumnIndex == -1)
            {
                innerColumnIndex = 0;
                continue;
            }

            if (rawRows[rowIndex][i] == ' ')
            {
                if (hitNumber)
                {
                    columns[outerColumnIndex][rowIndex] = columns[outerColumnIndex][rowIndex] + ' ';
                }
                else
                {
                    columns[outerColumnIndex][rowIndex] = ' ' + columns[outerColumnIndex][rowIndex];
                }
            }
            else
            {
                hitNumber = true;
            }

            if (++innerColumnIndex >= columnWidths[outerColumnIndex])
            {
                ++outerColumnIndex;
                innerColumnIndex = -1;
                hitNumber = false;
            }
        }
    }

}