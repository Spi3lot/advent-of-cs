namespace AdventOfCode.Year2024;

public partial record Day10
{

    private sealed class TopographicMap
    {

        public Vertex[,] Vertices { get; }

        private readonly ICollection<Vertex> _trailheads = [];


        public TopographicMap(string map)
        {

            string[] lines = map.Split()
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();

            Vertices = new Vertex[lines.Length, lines[0].Length];
            FillVertices(lines);
            SetNeighbors();
        }

        private void FillVertices(string[] lines)
        {

            for (int y = 0; y < Vertices.GetLength(0); y++)
            {
                for (int x = 0; x < Vertices.GetLength(1); x++)
                {
                    var vertex = new Vertex(lines[y][x] - '0');
                    Vertices[y, x] = vertex;
                    if (vertex.Height == 0) _trailheads.Add(vertex);
                }
            }
        }

        private void SetNeighbors()
        {
            for (int y = 0; y < Vertices.GetLength(0); y++)
            {
                for (int x = 0; x < Vertices.GetLength(1); x++)
                {
                    foreach (var (dx, dy) in Vertex.NeighborDirections)
                    {
                        if (x + dx < 0 || x + dx >= Vertices.GetLength(1) || y + dy < 0 || y + dy >= Vertices.GetLength(0)) continue;
                        var vertex = Vertices[y, x];
                        var neighbor = Vertices[y + dy, x + dx];
                        if (neighbor.Height - vertex.Height == 1) vertex.Neighbors.Add(neighbor);
                    }
                }
            }
        }

        public int SumTrailheadScores()
        {
            return _trailheads.Sum(trailhead => CalcScore(trailhead, new HashSet<Vertex>()));
        }

        public int SumTrailheadRatings()
        {
            return _trailheads.Sum(CalcRating);
        }

        public static int CalcScore(Vertex vertex, ISet<Vertex> exclude)
        {
            if (vertex.Height == 9 && !exclude.Contains(vertex))
            {
                exclude.Add(vertex);
                return 1;
            }

            return vertex.Neighbors.Sum(neighbor => CalcScore(neighbor, exclude));
        }

        public static int CalcRating(Vertex vertex)
        {
            if (vertex.Height == 9) return 1;
            return vertex.Neighbors.Sum(CalcRating);
        }


    }

}