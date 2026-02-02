using InstagramAnalyzer.Application.Interfaces;
using InstagramAnalyzer.Domain.Entities;

namespace InstagramAnalyzer.Application.Services;

public class RelationshipAnalyzer : IRelationshipAnalyzer
{
    public IEnumerable<InstagramProfile> GetNonFollowers(
        IEnumerable<InstagramProfile> followers,
        IEnumerable<InstagramProfile> following)
    {
        // Usamos StringComparer.OrdinalIgnoreCase para que 'User' sea igual a 'user'
        var followerUsernames = followers
            .Select(f => f.Username.Trim())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return following.Where(f => !followerUsernames.Contains(f.Username.Trim()));
    }

    public IEnumerable<InstagramProfile> GetFans(
        IEnumerable<InstagramProfile> followers,
        IEnumerable<InstagramProfile> following)
    {
        var followingUsernames = following
            .Select(f => f.Username.Trim())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return followers.Where(f => !followingUsernames.Contains(f.Username.Trim()));
    }

    public IEnumerable<InstagramProfile> GetMutuals(
        IEnumerable<InstagramProfile> followers,
        IEnumerable<InstagramProfile> following)
    {
        var followingUsernames = following
            .Select(f => f.Username.Trim())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return followers.Where(f => followingUsernames.Contains(f.Username.Trim()));
    }
}