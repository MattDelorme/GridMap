using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace GridMap
{
    public class SquareGridNodePathfinding : Pathfinding<Vector2>
    {
        public Map Map;

        protected override int StartCost { get { return 0; } }

        protected override IEnumerable<Vector2> GetNeighbours(Vector2 node)
        {
            List<Vector2> neighbours = new List<Vector2>();
            if (node.x > 0 && isPassable(Map, node.x - 1, node.y))
            {
                neighbours.Add(new Vector2(node.x - 1, node.y));
            }
            if (node.x < Map.Columns - 1 && isPassable(Map, node.x + 1, node.y))
            {
                neighbours.Add(new Vector2(node.x + 1, node.y));
            }
            if (node.y > 0 && isPassable(Map, node.x, node.y - 1))
            {
                neighbours.Add(new Vector2(node.x, node.y - 1));
            }
            if (node.y < Map.Rows - 1 && isPassable(Map, node.x, node.y + 1))
            {
                neighbours.Add(new Vector2(node.x, node.y + 1));
            }
            return neighbours;
        }

        protected override int GetCost(Vector2 currentNode, Vector2 neighbourNode)
        {
            return 1;
        }

        protected override int HeuristicCostEstimate(Vector2 startNode, Vector2 goalNode)
        {
            int xCost = Math.Abs((int)goalNode.x - (int)startNode.x);
            int yCost = Math.Abs((int)goalNode.y - (int)startNode.y);
            return xCost + yCost;
        }

        private bool isPassable(Map map, float x, float y)
        {
            return map[(int)x, (int)y] == 0;
        }
    }
}
