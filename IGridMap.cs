using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace GridMap
{
    public interface IGridMap
    {
        PriorityEvent<IntVector2> CellHighlighted { get; }
        PriorityEvent<IntVector2> CellSelected { get; }

        void CreateMap(Map map);
        Vector3 GetPosition(IntVector2 coordinates);
        int GetDistance(IntVector2 fromCoordinates, IntVector2 toCoordinates);
        int GetCost(IntVector2 fromCoordinates, IntVector2 toCoordinates);
        bool IsInBounds(IntVector2 coordinates);
        bool IsPassable(IntVector2 coordinates);
        List<IntVector2> FindPath(IntVector2 startCoordinates, IntVector2 destinationCoordinates);
        int GetTotalCost(IntVector2 startCoordinates, IntVector2 destinationCoordinates);
        int GetTotalCostOfPath(List<IntVector2> path);
        IEnumerable<IntVector2> GetAllCoordinates();
    }

    public interface IGridMap<T> : IGridMap
    {
        T GetValueAtCoordinates(IntVector2 coordinates);
    }
}
