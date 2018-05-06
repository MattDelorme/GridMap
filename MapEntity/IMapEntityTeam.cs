using System;
using System.Collections.Generic;
using Shared;

namespace GridMap
{
    public interface IMapEntityTeam
    {
        event Action<IMapEntityTeam, IMapEntity> OnMemberSelected;
        event Action OnMemberDeselected;

        int Allegiances { get; }

        void Init(IGridMap gridMap, List<IMapEntity> mapEntities);
        IMapEntity Find(IntVector2 coordinates);
        IEnumerable<IMapEntity> MapEntities { get; }
        IMapEntity SelectedMember { get; }

        void Select(IMapEntity member);
        void Deselect();
    }
}
