using System;
using Shared;

namespace GridMap
{
    public interface IMapEntityTeam
    {
        event Action<IMapEntity> OnMemberSelected;
        event Action OnMemberDeselected;

        int Allegiances { get; }

        void Init(IGridMap gridMap);
        IMapEntity Find(IntVector2 coordinates);

        void Select(IMapEntity member);
        void Deselect();
    }
}
