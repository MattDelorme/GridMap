using System;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace GridMap
{
    public interface IGridMap
    {
        event Action<IntVector2> CellHighlighted;
        event Action<IntVector2> CellSelected;

        void CreateMap(Map map);
        Vector3 GetPosition(IntVector2 coordinates);
        int GetCost(IntVector2 fromCoordinates, IntVector2 toCoordinates);
        bool IsInBounds(IntVector2 coordinates);
        bool IsPassable(IntVector2 coordinates);
        List<IntVector2> FindPath(IntVector2 startCoordinates, IntVector2 destinationCoordinates);
        int GetTotalCost(IntVector2 startCoordinates, IntVector2 destinationCoordinates);
        int GetTotalCostOfPath(List<IntVector2> path);
    }
}
