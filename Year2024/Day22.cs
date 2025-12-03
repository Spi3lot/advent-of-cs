namespace AdventOfCode.Year2024;

public partial record Day22 : AdventDay<Day22>
{

    private const int N = 2000;

    private readonly ulong[] _initialSecrets;

    public Day22()
    {
        _initialSecrets = [.. from line in Input.Split("\n")
                              where !string.IsNullOrWhiteSpace(line)
                              select ulong.Parse(line)];
    }

    private static ulong NthSecret(ulong initialSecret, ulong n)
    {
        ulong secret = initialSecret;

        for (ulong i = 0; i < n; i++)
        {
            secret = NextSecret(secret);
        }

        return secret;
    }

    private static ulong NextSecret(ulong secret)
    {
        secret = ((secret << 6) ^ secret) & 0xFFFFFF;
        secret = ((secret >> 5) ^ secret) & 0xFFFFFF;
        secret = ((secret << 11) ^ secret) & 0xFFFFFF;
        return secret;
    }

}