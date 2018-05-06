using System;
using System.Collections.Generic;
using Shared;

namespace GridMap
{
    public interface IMapEntity : IEntity
    {
        event Action<IntVector2> CoordinatesSet;

        float Scale { set; }
        IGridMap GridMap { get; set; }
        IntVector2 Coordinates { get; set; }
        List<IntVector2> MovementPath { get; set; }
    }
}
