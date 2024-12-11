using System.Numerics;

namespace AdventOfCode;

public record Day8 : AdventDay<Day8>
{

    private readonly Vector2 _gridDimensions;

    private readonly ILookup<char, Vector2> _antennaFrequenciesToPositions;

    public Day8()
    {
        string[] lines = Input.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();

        _gridDimensions = new Vector2(lines[0].Length, lines.Length);

        _antennaFrequenciesToPositions = lines.SelectMany(static (line, j) => line.ToCharArray()
                .Select((frequency, i) => (frequency, position: new Vector2(i, j)))
                .Where(static antenna => antenna.frequency != '.'))
            .ToLookup(static antenna => antenna.frequency, static antenna => antenna.position);
    }

    public override void SolvePart1()
    {
        SolvePart(1..1);
    }

    public override void SolvePart2()
    {
        SolvePart(0..int.MaxValue);
    }

    /// <param name="harmonics">Both start and end harmonic indices are inclusive</param>
    private void SolvePart(Range harmonics)
    {
        var containedAntinodes = new HashSet<Vector2>();

        foreach (var antennaPositions in _antennaFrequenciesToPositions)
        {
            for (int i = 0; i < antennaPositions.Count(); i++)
            {
                var antennaPosition1 = antennaPositions.ElementAt(i);

                for (int j = i + 1; j < antennaPositions.Count(); j++)
                {
                    var antennaPosition2 = antennaPositions.ElementAt(j);
                    AddAntinodesIfContainedInGrid(harmonics, (antennaPosition1, antennaPosition2), containedAntinodes);
                }
            }
        }

        Console.WriteLine(containedAntinodes.Count);
    }

    private void AddAntinodesIfContainedInGrid(Range harmonics, (Vector2, Vector2) antennaPositions, HashSet<Vector2> containedAntinodes)
    {
        var difference = antennaPositions.Item2 - antennaPositions.Item1;

        for (int i = harmonics.Start.Value; i <= harmonics.End.Value; i++)
        {
            var antinodePosition1 = antennaPositions.Item1 - difference * i;
            var antinodePosition2 = antennaPositions.Item2 + difference * i;
            var gridContains = (GridContains(antinodePosition1), GridContains(antinodePosition2));

            if (!gridContains.Item1 && !gridContains.Item2) break;
            if (gridContains.Item1) containedAntinodes.Add(antinodePosition1);
            if (gridContains.Item2) containedAntinodes.Add(antinodePosition2);
        }
    }

    private bool GridContains(Vector2 position)
    {
        return position.X >= 0 && position.X <= _gridDimensions.X - 1 && position.Y >= 0 && position.Y <= _gridDimensions.Y - 1;
    }

}