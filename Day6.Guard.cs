namespace AdventOfCode;

public partial record Day6
{

    private sealed class Guard
    {

        private readonly (int x, int y) _originalDirection;

        private readonly CellConditions[][] _originalGrid;
        private readonly (int x, int y) _originalPosition;

        public Guard(CellConditions[][] grid)
        {
            _originalGrid = grid;
            bool foundGuard = false;

            for (int j = 0; !foundGuard && j < grid.Length; j++)
            {
                var row = grid[j];

                for (int i = 0; i < row.Length; i++)
                {
                    var flags = grid[j][i];

                    if (flags.HasFlag(Day6.CellConditions.Visited))
                    {
                        _originalPosition = (i, j);
                        _originalDirection = GetDirectionFromFlag(flags);
                        foundGuard = true;
                        break;
                    }
                }
            }

            Reset();
        }

        public CellConditions[][] Grid { get; private set; }

        public (int x, int y) Position { get; private set; }

        public (int x, int y) Direction { get; private set; }

        public void Reset()
        {
            Grid = _originalGrid.Select(inner => inner.ToArray()).ToArray();
            Position = _originalPosition;
            Direction = _originalDirection;
        }

        /**
         * Returns null whenever the guard gets caught in a loop
         */
        public int? PatrolAndCountUniquePositions()
        {
            int count = 1;

            while (Move())
            {
                if (WasInCurrentSituationBefore()) return null;

                if (!VisitedCurrentPositionAlready()) count++;

                UpdateFlagsForCurrentPosition();
            }

            return count;
        }

        private bool VisitedCurrentPositionAlready()
        {
            return Grid[Position.y][Position.x].HasFlag(Day6.CellConditions.Visited);
        }

        private bool WasInCurrentSituationBefore()
        {
            return Grid[Position.y][Position.x].HasFlag(GetFlagFromDirection(Direction));
        }

        private bool Move()
        {
            var movedPosition = Position;
            movedPosition.x += Direction.x;
            movedPosition.y += Direction.y;

            if (!IsOnGrid(movedPosition)) return false;

            if (Grid[movedPosition.y][movedPosition.x].HasFlag(Day6.CellConditions.Obstacle))
            {
                RotateClockwise();
            }
            else
            {
                Position = movedPosition;
            }

            return true;
        }

        private void UpdateFlagsForCurrentPosition()
        {
            Grid[Position.y][Position.x] |= Day6.CellConditions.Visited;
            Grid[Position.y][Position.x] |= GetFlagFromDirection(Direction);
        }

        private bool IsOnGrid((int x, int y) position)
        {
            return position.y >= 0 && position.y < Grid.Length && position.x >= 0 &&
                   position.x < Grid[position.y].Length;
        }

        private void RotateClockwise()
        {
            Direction = (-Direction.y, Direction.x);
        }

    }

}