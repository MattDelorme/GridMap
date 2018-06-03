using System;
using System.Collections.Generic;
using Shared;

namespace GridMap
{
    public struct MapTeamAndEntity
    {
        public readonly IMapEntityTeam MapEntityTeam;
        public readonly IMapEntity MapEntity;

        public MapTeamAndEntity(IMapEntityTeam mapEntityTeam, IMapEntity mapEntity)
        {
            MapEntity = mapEntity;
            MapEntityTeam = mapEntityTeam;
        }
    }

    public interface IMapEntityTeam
    {
        event Action<IMapEntityTeam, IMapEntity> OnMemberSelected;
        event Action OnMemberDeselected;
        PriorityEvent<MapTeamAndEntity> OnMemberInteracted { get; }

        int Allegiances { get; }

        void Init(IGridMap gridMap, List<IMapEntity> mapEntities);
        IMapEntity Find(IntVector2 coordinates);
        IEnumerable<IMapEntity> MapEntities { get; }
        IMapEntity SelectedMember { get; }

        void Select(IMapEntity member);
        void Deselect();
        void Interact(IMapEntity member);
    }
}
