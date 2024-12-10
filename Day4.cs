namespace AdventOfCode;

public record Day4 : AdventDay<Day4>
{

    public override void SolvePart1()
    {
        Console.WriteLine(CountWordOccurrencesInGrid("XMAS", Input.Split()));
    }

    public override void SolvePart2()
    {
        Console.WriteLine(CountXShapedMases(Input.Split()));
    }

    private static int CountWordOccurrencesInGrid(string word, string[] grid)
    {
        int count = 0;

        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        bool foundWord = true;

                        for (int i = 0; i < word.Length; i++)
                        {
                            int offsetX = x + dx * i;
                            int offsetY = y + dy * i;

                            if (offsetY < 0 || offsetY >= grid.Length || offsetX < 0 ||
                                offsetX >= grid[offsetY].Length ||
                                grid[offsetY][offsetX] != word[i])
                            {
                                foundWord = false;
                                break;
                            }
                        }

                        if (foundWord) count++;
                    }
                }
            }
        }

        return count;
    }

    public static int CountXShapedMases(string[] grid)
    {
        int count = 0;

        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                if (IsMasX(grid, (x, y)))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static bool IsMasX(string[] grid, (int, int) center)
    {
        if (grid[center.Item2][center.Item1] != 'A') // 'A' => "mirror axis" aka center
        {
            return false;
        }

        var deltas = new[] { (-1, -1), (1, -1) };

        foreach (var delta in deltas)
        {
            var start = (center.Item1 + delta.Item1, center.Item2 + delta.Item2);
            var end = (center.Item1 - delta.Item1, center.Item2 - delta.Item2);

            if (!IsOnGrid(start, grid) || !IsOnGrid(end, grid) ||
                grid[start.Item2][start.Item1] != MirroredChar(grid[end.Item2][end.Item1]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsOnGrid((int, int) position, string[] grid)
    {
        return position.Item2 >= 0 && position.Item2 < grid.Length && position.Item1 >= 0 &&
               position.Item1 < grid[position.Item2].Length;
    }


    private static char? MirroredChar(char c)
    {
        return c switch
        {
            'M' => 'S',
            'S' => 'M',
            _ => null
        };
    }

}