using Shared;

namespace GridMap
{
    public interface ICoverModifierManager
    {
        int GetCoverModifier(IMapEntity attacker, IMapEntity target);
        bool ProvidesCover(IntVector2 coordinates);
        bool HasCover(IMapEntity attacker, IMapEntity target);
    }
}
