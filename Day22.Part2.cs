namespace AdventOfCode;

public partial record Day22
{

    private const int DifferenceBufferLength = 4;

    public override void SolvePart2()
    {
        Dictionary<int, int> revenues = [];
        byte[,] bananaAmounts = GenerateBananaAmounts();
        var currentDifferences = new DifferenceBuffer(DifferenceBufferLength);
        int impossibleCount = 0;

        for (int j = 0; j < bananaAmounts.GetLength(0); j++)
        {
            for (int i = 1; i < bananaAmounts.GetLength(1); i++)
            {
                currentDifferences.Shift(bananaAmounts[j, i] - bananaAmounts[j, i - 1]);
                if (i > DifferenceBufferLength) revenues[currentDifferences.Base19Representation] = 0;
            }
        }

        foreach (int base19 in revenues.Keys)
        {
            if (!DifferenceBuffer.FromBase19(base19, DifferenceBufferLength).IsPossible())
            {
                impossibleCount++; continue;
            }

            for (int j = 0; j < bananaAmounts.GetLength(0); j++)
            {
                for (int i = 1; i < bananaAmounts.GetLength(1); i++)
                {
                    currentDifferences.Shift(bananaAmounts[j, i] - bananaAmounts[j, i - 1]);

                    if (i > DifferenceBufferLength && currentDifferences.Base19Representation == base19)
                    {
                        revenues[base19] += bananaAmounts[j, i];
                    }
                }
            }

            if (base19 % 100 == 0) Console.WriteLine(base19);
        }

        Console.WriteLine(string.Join(',', revenues));
        Console.WriteLine(impossibleCount);
        Console.WriteLine(revenues.Count);
        Console.WriteLine(Math.Pow(19, 4) - revenues.Count);
        Console.WriteLine(revenues.Values.Max());
    }

    private byte[,] GenerateBananaAmounts()
    {
        byte[,] bananaAmounts = new byte[_initialSecrets.Length, N + 1];

        foreach (var (buyerIndex, initialSecret) in _initialSecrets.Index())
        {
            ulong secret = initialSecret;

            for (int i = 0; i <= N; i++)
            {
                bananaAmounts[buyerIndex, i] = (byte)(secret % 10);
                secret = NextSecret(secret);
            }
        }

        return bananaAmounts;
    }

}