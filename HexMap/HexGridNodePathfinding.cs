using System;
using System.Collections.Generic;
using Pathfinding;
using Shared;

namespace GridMap
{
    public class HexGridNodePathfinding : Pathfinding<HexCoordinates>
    {
        public Map Map;

        protected override int StartCost { get { return 0; } }

        protected override IEnumerable<HexCoordinates> GetNeighbours(HexCoordinates node)
        {
            List<HexCoordinates> neighbours = new List<HexCoordinates>();
    
            List<HexCoordinates> potentialNeighbours = node.GetNeighbours();
    
            for (int i = 0; i < potentialNeighbours.Count; i++)
            {
                IntVector2 mapCoordinates = potentialNeighbours[i].ToMapCoordinates();
                if (Map.IsInBounds(mapCoordinates) && isPassable(mapCoordinates, Map))
                {
                    neighbours.Add(potentialNeighbours[i]);
                }
            }
            return neighbours;
        }

        protected override int GetCost(HexCoordinates currentNode, HexCoordinates neighbourNode)
        {
            IntVector2 mapCoordinates = neighbourNode.ToMapCoordinates();
            return Map[mapCoordinates.X, mapCoordinates.Y];
        }

        protected override int HeuristicCostEstimate(HexCoordinates startNode, HexCoordinates goalNode)
        {
            return startNode.DistanceTo(goalNode);
        }

        private static bool isPassable(IntVector2 mapCoordinates, Map map)
        {
            return map[mapCoordinates.X, mapCoordinates.Y] > 0;
        }
    }
}
