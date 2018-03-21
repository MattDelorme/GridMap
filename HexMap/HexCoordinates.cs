using System.Collections.Generic;
using Shared;

namespace GridMap
{
    public struct HexCoordinates
    {
        public enum Direction { W, NW, NE, E, SE, SW }

        private readonly IntVector2 coordinates;

        public int X
        {
            get
            {
                return coordinates.X;
            }
        }

        public int Z
        {
            get
            {
                return coordinates.Y;
            }
        }

        public int Y
        {
            get
            {
                return -X - Z;
            }
        }

        public HexCoordinates(int x, int z)
        {
            coordinates = new IntVector2(x, z);
        }

        public HexCoordinates AddX(int x)
        {
            return new HexCoordinates(X + x, Z);
        }

        public HexCoordinates AddY(int y)
        {
            return new HexCoordinates(X - y, Z + y);
        }

        public HexCoordinates AddZ(int z)
        {
            return new HexCoordinates(X, Z + z);
        }

        public HexCoordinates Add(int value, Direction direction)
        {
            switch (direction)
            {
                case Direction.W:
                    return AddX(-1 * value);
                case Direction.NW:
                    return AddY(value);
                case Direction.NE:
                    return AddZ(value);
                case Direction.E:
                    return AddX(value);
                case Direction.SE:
                    return AddY(-1 *value);
                default:
                    return AddZ(-1 * value);
            }
        }

        public List<HexCoordinates> GetNeighbours()
        {
            var neighbours = new List<HexCoordinates>();
            for (int i = 0; i < 6; i++)
            {
                neighbours.Add(Add(1, (Direction)i));
            }
            return neighbours;
        }

        public List<HexCoordinates> GetNeighbours(int radius)
        {
            var distances = new Dictionary<HexCoordinates, int>();
            distances.Add(this, 0);
            var frontier = new Queue<HexCoordinates>();
            frontier.Enqueue(this);
            while (frontier.Count > 0)
            {
                HexCoordinates current = frontier.Dequeue();
                if (distances[current] < radius)
                {
                    List<HexCoordinates> neighbours = current.GetNeighbours();
                    for (int i = 0; i < neighbours.Count; i++)
                    {
                        if (!distances.ContainsKey(neighbours[i]))
                        {
                            frontier.Enqueue(neighbours[i]);
                            distances.Add(neighbours[i], distances[current] + 1);
                        }
                    }
                }
            }
            return new List<HexCoordinates>(distances.Keys);
        }

        public static HexCoordinates FromMapCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }

        public static HexCoordinates FromMapCoordinates(IntVector2 coordinates)
        {
            return new HexCoordinates(coordinates.X - coordinates.Y / 2, coordinates.Y);
        }

        public IntVector2 ToMapCoordinates()
        {
            return new IntVector2(-(-X - Z / 2), Z);
        }

        public int DistanceTo(HexCoordinates target)
        {
            return ((X < target.X ? target.X - X : X - target.X) +
                    (Y < target.Y ? target.Y - Y : Y - target.Y) +
                    (Z < target.Z ? target.Z - Z : Z - target.Z)) / 2;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HexCoordinates))
            {
                return false;
            }
            var hexCoordinates = (HexCoordinates)obj;
            return coordinates.Equals(hexCoordinates.coordinates);
        }

        public bool Equals(HexCoordinates hexCoordinates)
        {
            return coordinates.Equals(hexCoordinates.coordinates);
        }

        public static bool operator ==(HexCoordinates left, HexCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HexCoordinates left, HexCoordinates right)
        {
            return !(left.Equals(right));
        }

        public override int GetHashCode()
        {
            return coordinates.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }
    }
}
