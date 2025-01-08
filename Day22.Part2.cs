namespace AdventOfCode;

public partial record Day22
{

    private const int DifferenceBufferLength = 4;

    public override void SolvePart2()
    {
        var encounteredDifferences = new HashSet<int>();
        var currentDifferences = new DifferenceBuffer(DifferenceBufferLength);
        int[] revenues = new int[currentDifferences.CombinationCount];
        byte[,] bananaAmounts = GenerateBananaAmounts();

        for (int j = 0; j < bananaAmounts.GetLength(0); j++)
        {
            encounteredDifferences.Clear();

            for (int i = 1; i < bananaAmounts.GetLength(1); i++)
            {
                currentDifferences.Shift(bananaAmounts[j, i] - bananaAmounts[j, i - 1]);

                if (i >= DifferenceBufferLength && encounteredDifferences.Add(currentDifferences.Base19))
                {
                    revenues[currentDifferences.Base19] += bananaAmounts[j, i];
                }
            }
        }

        var max = revenues.Index().MaxBy(indexedItem => indexedItem.Item);
        var maxBuffer = DifferenceBuffer.FromBase19(max.Index, DifferenceBufferLength);

        for (int i = 0; i < DifferenceBufferLength; i++)
        {
            Console.Write($"{maxBuffer[i]} ");
        }

        Console.WriteLine();
        Console.WriteLine(max.Item); // ~3017s~ / 0,14s ~= 21550x speedup
    }

    private byte[,] GenerateBananaAmounts()
    {
        byte[,] bananaAmounts = new byte[_initialSecrets.Length, N + 1];

        foreach (var (buyerIndex, initialSecret) in _initialSecrets.Index())
        {
            ulong secret = initialSecret;

            for (int i = 0; i <= N; i++)
            {
                bananaAmounts[buyerIndex, i] = (byte) (secret % 10);
                secret = NextSecret(secret);
            }
        }

        return bananaAmounts;
    }

}