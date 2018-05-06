using System;
using Shared;

namespace GridMap
{
    public interface ILineOfSightManager
    {
        bool HasLineOfSight(IMapEntity observer, IntVector2 targetCoordinates);
    }
}
