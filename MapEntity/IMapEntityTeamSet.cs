using System;
using System.Collections.Generic;
using Shared;

namespace GridMap
{
    public interface IMapEntityTeamSet
    {
        event Action<IMapEntityTeam> CurrentlyActiveTeamChanged;

        IEnumerable<IMapEntityTeam> MapEntityTeams { get; }
        IMapEntityTeam CurrentlyActiveTeam { get; }

        HashSet<IntVector2> GetAllEntityCoordinates();

        void ChangeActiveTeam();

        bool IsTeamCurrentlyEnemy(IMapEntityTeam team);
        bool IsTeamCurrentlyAlly(IMapEntityTeam team);
    }
}
