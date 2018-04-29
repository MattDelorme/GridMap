using Shared;

namespace GridMap
{
    public interface IMapEntity
    {
        IGridMap GridMap { get; set; }
        float Scale { set; }
        IntVector2 Coordinates { get; }

        void SetLocation(IntVector2 coordinates);
        void MoveTo(IntVector2 coordinates);
    }
}

