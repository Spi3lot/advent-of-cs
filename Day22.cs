namespace AdventOfCode;

public record Day22 : AdventDay<Day22>
{

    public override void SolvePart1()
    {
        ulong sum = Input.Split("\n")
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(ulong.Parse)
            .Select(initialSecret => NthSecret(initialSecret, 2000))
            .Aggregate(0uL, ((accumulate, secret) => accumulate + secret));
        
        Console.WriteLine(sum);
    }

    public override void SolvePart2() { }

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