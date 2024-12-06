
namespace AdventOfCode;

public partial record Day6
{

    private class Guard
    {

        private readonly GridFlag[][] Grid;

        private (int x, int y) Position;

        private (int x, int y) Direction;

        public Guard(GridFlag[][] grid)
        {
            Grid = grid;
            bool foundGuard = false;

            for (int j = 0; !foundGuard && j < grid.Length; j++)
            {
                var row = grid[j];

                for (int i = 0; i < row.Length; i++)
                {
                    var flags = grid[j][i];

                    if (flags.HasFlag(GridFlag.Visited))
                    {
                        Position = (i, j);
                        Direction = GetDirectionFromFlag(flags);
                        foundGuard = true;
                        break;
                    }
                }
            }
        }

        public bool VisitedCurrentPositionAlready()
        {
            return Grid[Position.y][Position.x].HasFlag(GridFlag.Visited);
        }

        public bool WasInCurrentSituationBefore()
        {
            return Grid[Position.y][Position.x].HasFlag(GetFlagFromDirection(Direction));
        }

        public bool Move()
        {
            var movedPosition = Position;
            movedPosition.x += Direction.x;
            movedPosition.y += Direction.y;

            if (IsOnGrid(movedPosition))
            {
                if (Grid[Position.y][Position.x].HasFlag(GridFlag.Obstacle))
                {
                    RotateClockwise();
                    return true;
                }

                Position = movedPosition;
                return true;
            }

            return false;
        }

        public void UpdateFlagsForCurrentPosition()
        {
            Grid[Position.y][Position.x] |= GridFlag.Visited;
            Grid[Position.y][Position.x] |= GetFlagFromDirection(Direction);
        }

        private bool IsOnGrid((int x, int y) position)
        {
            return position.y >= 0 && position.y < Grid.Length && position.x >= 0 && position.x < Grid[position.y].Length;
        }

        private void RotateClockwise()
        {
            (Direction.x, Direction.y) = (Direction.y, -Direction.x);
        }
    }

}