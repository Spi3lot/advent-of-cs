using System.Text.RegularExpressions;

namespace AdventOfCode.Year2025;

public partial record Day6() : AdventDay<Day6>(2025)
{

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhiteSpaceRegex { get; }

    private static readonly Dictionary<string, Func<long, long, long>> operatorFunctions = new()
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
                .ToArray())
            .ToArray();

        long grandTotal = rows[^1]
            .Select(op => operatorFunctions[op])
            .Index()
            .Sum(op => columns[op.Index].Aggregate((result, current) => op.Item(result, current)));

        Console.WriteLine(grandTotal);
    } 


    public override void SolvePart2()
    {
        string[][] rows = Input.Trim()
            .Split('\n')
            .Select(line => WhiteSpaceRegex.Split(line.Trim()))
            .ToArray();

        string[][] columns = Enumerable.Range(0, rows[0].Length)
            .Select(i => Enumerable.Range(0, rows.Length - 1)
                .Select(j => rows[j][i])
                .ToArray())
            .ToArray();
        
        int[] columnWidths = columns
            .Select(column => column.Max(num => num.Length))
            .ToArray();
        
        string[][] transposed = columns.Index()
                .Select(column => Enumerable.Range(0, columnWidths[column.Index])
                    .Select(i => )
                    .Aggregate()
                .ToArray())
            .ToArray();

        long grandTotal = rows[^1]
            .Select(op => operatorFunctions[op])
            .Index()
            .Sum(op => transposed[op.Index]
                .Select(long.Parse)
                .Aggregate((result, current) => op.Item(result, current)));

        Console.WriteLine(grandTotal);
    }

}