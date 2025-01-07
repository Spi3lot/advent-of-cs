using System;

namespace AdventOfCode;

public partial record Day22
{

    private const int DifferenceBufferLength = 4;

    public override void SolvePart2()
    {
        byte[,] bananaAmounts = GenerateBananaAmounts();
        var currentDifferences = new DifferenceBuffer(DifferenceBufferLength);

        var revenues = FillRevenueKeysAndDifferences(
            currentDifferences,
            bananaAmounts,
            out sbyte[,] differences
        );

        foreach (int base19 in revenues.Keys)
        {
            for (int j = 0; j < differences.GetLength(0); j++)
            {
                for (int i = 0; i < differences.GetLength(1); i++)
                {
                    currentDifferences.Shift(differences[j, i]);

                    if (currentDifferences.Base19 == base19 && i + 1 >= DifferenceBufferLength)
                    {
                        revenues[base19] += bananaAmounts[j, i + 1];
                        break;
                    }
                }
            }
        }

        var max = revenues.MaxBy(pair => pair.Value);
        var maxBuffer = DifferenceBuffer.FromBase19(max.Key, DifferenceBufferLength);

        for (int i = 0; i < DifferenceBufferLength; i++)
        {
            Console.Write($"{maxBuffer[i]} "); // 1 -1 0 1
        }

        Console.WriteLine();
        Console.WriteLine(max.Value); // 1490; Took only 3016,9640234s
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

    private static Dictionary<int, int> FillRevenueKeysAndDifferences(
        DifferenceBuffer buffer,
        byte[,] bananaAmounts,
        out sbyte[,] differences
    )
    {
        Dictionary<int, int> revenues = [];

        differences = new sbyte[
            bananaAmounts.GetLength(0),
            bananaAmounts.GetLength(1) - 1
        ];

        for (int j = 0; j < differences.GetLength(0); j++)
        {
            for (int i = 0; i < differences.GetLength(1); i++)
            {
                sbyte difference = (sbyte) (bananaAmounts[j, i + 1] - bananaAmounts[j, i]);
                differences[j, i] = difference;
                buffer.Shift(difference);
                if (i + 1 >= DifferenceBufferLength) revenues[buffer.Base19] = 0;
            }
        }

        return revenues;
    }

}