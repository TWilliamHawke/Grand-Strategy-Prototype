namespace Battlefield
{
    public enum Directions
    {
        north,
        northEast,
        east,
        southEast,
        south,
        southWest,
        west,
        northWest
    }

    public static class DirectionsMethods
    {
        public static bool IsDiagonal(this Directions direction)
        {
            return (int)direction%2 > 0;
        }

        public static bool IsHorizontal(this Directions direction)
        {
            return direction == Directions.east || direction == Directions.west;
        }
    }
}