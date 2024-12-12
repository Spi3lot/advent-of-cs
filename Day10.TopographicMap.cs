using System.Collections.Frozen;

namespace AdventOfCode;

public partial record Day10
{

    private sealed class TopographicMap
    {

        public Vertex[,] Map { get; }

        public TopographicMap(string map)
        {
            string[] lines = map.Split()
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();

            Map = new Vertex[lines.Length, lines[0].Length];
            
            for (int y = 0; y < Map.GetLength(0); y++)
            {
                for (int x = 0; x < Map.GetLength(1); x++)
                {
                    Map[y, x] = new Vertex(lines[y][x] - '0');
                }
            }
            
            for (int y = 0; y < Map.GetLength(0); y++)
            {
                for (int x = 0; x < Map.GetLength(1); x++)
                {
                    foreach (var (dx, dy) in Vertex.NeighborDirections)
                    {
                        var vertex = Map[y, x];
                        // TODO: check boundaries
                        var neighbor = Map[y + dy, x + dx];

                        if (neighbor.Height - vertex.Height == 1)
                        {
                            vertex.Neighbors.Add(neighbor);
                        }
                    }
                }
            }
        }

    }

}