using System;
using Shared;

namespace GridMap
{
    [Serializable]
    public class Map
    {
        public int[] OneDimensionalMap;
        public int Columns;
        public int Rows;

        public int this[int column, int row]
        {
            get { return OneDimensionalMap[row * Columns + column]; }
            set { OneDimensionalMap[row * Columns + column] = value; }
        }

        public Map(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;
            OneDimensionalMap = new int[columns * rows];
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 &&
            x < Columns &&
            y >= 0 &&
            y < Rows;
        }

        public bool IsInBounds(IntVector2 mapCoordinates)
        {
            return IsInBounds(mapCoordinates.X, mapCoordinates.Y);
        }
    }
}
