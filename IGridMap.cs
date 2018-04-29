using System;
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
        int GetTotalCost(IntVector2 startCoordinates, IntVector2 destinationCoordinates);
    }
}
