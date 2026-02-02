using InstagramAnalyzer.Domain.Entities;

namespace InstagramAnalyzer.Application.Interfaces;

public interface IRelationshipAnalyzer
{
    /// <summary>
    /// Compara la lista de seguidos contra la de seguidores para encontrar 
    /// a los usuarios que no te devuelven el follow.
    /// </summary>
    IEnumerable<InstagramProfile> GetNonFollowers(
        IEnumerable<InstagramProfile> followers,
        IEnumerable<InstagramProfile> following);

    /// <summary>
    /// Encuentra a los usuarios que te siguen pero tú no sigues (tus fans).
    /// </summary>
    IEnumerable<InstagramProfile> GetFans(
        IEnumerable<InstagramProfile> followers,
        IEnumerable<InstagramProfile> following);

    /// <summary>
    /// Encuentra a los usuarios que se siguen mutuamente.
    /// </summary>
    IEnumerable<InstagramProfile> GetMutuals(
        IEnumerable<InstagramProfile> followers,
        IEnumerable<InstagramProfile> following);
}