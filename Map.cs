using Shared;

namespace GridMap
{
    public struct Map
    {
        public int[] OneDimensionalMap;
        public int Rows;
        public int Columns;
    
        private int[,] twoDimensionalMap;

        public int[,] TwoDimensionalMap {
            get {
                if (twoDimensionalMap == null)
                {
                    twoDimensionalMap = new int[Columns, Rows];
                    for (int i = 0; i < OneDimensionalMap.Length && i < Rows * Columns; i++)
                    {
                        int row = i / Columns;
                        int column = i % Columns;
                        twoDimensionalMap[column, row] = OneDimensionalMap[i];
                    }
                }
                return twoDimensionalMap;
            }
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
