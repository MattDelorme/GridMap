using Shared;
using UnityEngine;

namespace GridMap
{
    public interface IGridMap
    {
        Vector3 GetPosition(IntVector2 coordinates);
    }
}
