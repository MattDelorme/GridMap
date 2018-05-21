using Shared;

namespace GridMap
{
    public interface ILineOfSightManager
    {
        bool HasLineOfSight(IntVector2 observerCoordinates, IntVector2 targetCoordinates);
        bool BlocksLineOfSight(IntVector2 coordinates);
    }
}
