namespace AdventOfCode;

public partial record Day22
{

    private const int DifferenceBufferLength = 4;

    public override void SolvePart2()
    {
        byte[,] bananaAmounts = GenerateBananaAmounts();
        var currentDifferences = new DifferenceBuffer(DifferenceBufferLength);
        var revenues = FillRevenueKeys(bananaAmounts, currentDifferences);
        int count = 0;

        foreach (int base19 in revenues.Keys)
        {
            for (int j = 0; j < bananaAmounts.GetLength(0); j++)
            {
                for (int i = 1; i < bananaAmounts.GetLength(1); i++)
                {
                    currentDifferences.Shift(bananaAmounts[j, i] - bananaAmounts[j, i - 1]);
                    if (count <= DifferenceBufferLength || currentDifferences.Base19Representation != base19) continue;
                    revenues[base19] += bananaAmounts[j, i];
                    break;
                }
            }

            if (count++ % 10 == 0) Console.WriteLine((double) count / revenues.Count);
        }

        var max = revenues.MaxBy(pair => pair.Value);
        Console.WriteLine(string.Join(", ", DifferenceBuffer.FromBase19(max.Key, DifferenceBufferLength).Differences));
        Console.WriteLine(max.Value);
    }

    private static Dictionary<int, int> FillRevenueKeys(byte[,] bananaAmounts, DifferenceBuffer differences)
    {
        Dictionary<int, int> revenues = [];

        for (int j = 0; j < bananaAmounts.GetLength(0); j++)
        {
            for (int i = 1; i < bananaAmounts.GetLength(1); i++)
            {
                differences.Shift(bananaAmounts[j, i] - bananaAmounts[j, i - 1]);
                if (i > DifferenceBufferLength) revenues[differences.Base19Representation] = 0;
            }
        }

        return revenues;
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