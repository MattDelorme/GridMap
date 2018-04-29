using System.Collections.Generic;

namespace GridMap
{
    public interface IMapEntityTeamSet
    {
        IEnumerable<IMapEntityTeam> MapEntityTeams { get; }
        IMapEntityTeam CurrentlyActiveTeam { get; }

        void ChangeActiveTeam();
    }
}
