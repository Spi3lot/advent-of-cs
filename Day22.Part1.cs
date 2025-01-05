namespace AdventOfCode;

public partial record Day22
{

    public override void SolvePart1()
    {
        ulong sum = _initialSecrets
                    .Select(initialSecret => NthSecret(initialSecret, N))
                    .Aggregate(0uL, (accumulate, secret) => accumulate + secret);

        Console.WriteLine(sum);
    }

}