using System.Collections.Generic;
using Shared;
using UnityEngine;
using System;

namespace GridMap
{
    public enum HexDirection { W, NW, NE, E, SE, SW }

    public struct HexCoordinates
    {
        private readonly IntVector2 coordinates;

        public int X { get { return coordinates.X; } }

        public int Z { get { return coordinates.Y; } }

        public int Y { get { return -X - Z; } }

        public IntVector2 MapCoordinates { get { return new IntVector2(-(-X - Z / 2), Z); } }

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

        public HexCoordinates Add(int value, HexDirection direction)
        {
            switch (direction)
            {
                case HexDirection.W:
                    return AddX(-1 * value);
                case HexDirection.NW:
                    return AddY(value);
                case HexDirection.NE:
                    return AddZ(value);
                case HexDirection.E:
                    return AddX(value);
                case HexDirection.SE:
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
                neighbours.Add(Add(1, (HexDirection)i));
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

        public int DistanceTo(HexCoordinates target)
        {
            return ((X < target.X ? target.X - X : X - target.X) +
                    (Y < target.Y ? target.Y - Y : Y - target.Y) +
                    (Z < target.Z ? target.Z - Z : Z - target.Z)) / 2;
        }

        public static Vector3 Lerp(HexCoordinates a, HexCoordinates b, float t)
        {
            return Vector3.Lerp(new Vector3(a.X, a.Y, a.Z), new Vector3(b.X, b.Y, b.Z), t);
        }

        public static HexCoordinates Round(Vector3 floatHexCoordinates)
        {
            int x = (int)Mathf.Round(floatHexCoordinates.x);
            int y = (int)Mathf.Round(floatHexCoordinates.y);
            int z = (int)Mathf.Round(floatHexCoordinates.z);

            float xDelta = Mathf.Abs((float)x - floatHexCoordinates.x);
            float yDelta = Mathf.Abs((float)y - floatHexCoordinates.y);
            float zDelta = Mathf.Abs((float)z - floatHexCoordinates.z);

            if (xDelta > yDelta && xDelta > zDelta)
            {
                x = -y - z;
            }
            else if (yDelta > zDelta)
            {
                y = -x - z;
            }
            else
            {
                z = -x - y;
            }

            return new HexCoordinates(x, z);
        }

        public static List<HexCoordinates> GetLine(HexCoordinates start, HexCoordinates end)
        {
            int distance = start.DistanceTo(end);
            List<HexCoordinates> results = new List<HexCoordinates>();
            for (int i = 1; i <= distance; i++)
            {
                var floatHexCoordinates = Lerp(start, end, (float)1 / (float)distance * (float)i);
                results.Add(Round(floatHexCoordinates));
            }

            return results;
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
