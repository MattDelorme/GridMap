using System.Collections.Generic;
using Pathfinding;
using Shared;

namespace GridMap
{
    public class HexGridNodePathfinding : Pathfinding<HexCoordinates>
    {
        private IGridMap gridMap;

        protected override int StartCost { get { return 0; } }

        public HexGridNodePathfinding(IGridMap gridMap)
        {
            this.gridMap = gridMap;
        }

        protected override IEnumerable<HexCoordinates> GetNeighbours(HexCoordinates node)
        {
            List<HexCoordinates> neighbours = new List<HexCoordinates>();
    
            List<HexCoordinates> potentialNeighbours = node.GetNeighbours();
    
            for (int i = 0; i < potentialNeighbours.Count; i++)
            {
                IntVector2 mapCoordinates = potentialNeighbours[i].MapCoordinates;
                if (gridMap.IsInBounds(mapCoordinates) && gridMap.IsPassable(mapCoordinates))
                {
                    neighbours.Add(potentialNeighbours[i]);
                }
            }
            return neighbours;
        }

        protected override int GetCost(HexCoordinates currentNode, HexCoordinates neighbourNode)
        {
            return gridMap.GetCost(currentNode.MapCoordinates, neighbourNode.MapCoordinates);
        }

        protected override int HeuristicCostEstimate(HexCoordinates startNode, HexCoordinates goalNode)
        {
            return startNode.DistanceTo(goalNode);
        }
    }
}
