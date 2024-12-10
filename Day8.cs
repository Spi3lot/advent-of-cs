namespace AdventOfCode;

public record Day8 : AdventDay<Day8>
{

    public override void SolvePart1()
    {
        var antennaPositions = Input.Split()
            .SelectMany(static (line, j) => line.ToCharArray()
                .Select((frequency, i) => (frequency, position: (i, j)))
                .Where(antenna => antenna.frequency != '.'))
            .ToLookup(antenna => antenna.frequency, antenna => antenna.position);
    }

    public override void SolvePart2() { }

}