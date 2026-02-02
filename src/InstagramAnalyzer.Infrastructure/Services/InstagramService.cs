using System.IO.Compression;
using System.Text.Json;
using InstagramAnalyzer.Application.Interfaces;
using InstagramAnalyzer.Domain.Entities;
using InstagramAnalyzer.Domain.Enums;
using InstagramAnalyzer.Infrastructure.Models;

namespace InstagramAnalyzer.Infrastructure.Services;

public class InstagramService : IInstagramService
{
    public async Task<IEnumerable<InstagramProfile>> GetProfilesFromZipAsync(Stream zipStream, RelationshipType type)
    {
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read, leaveOpen: true);
        var profiles = new List<InstagramProfile>();

        // 1. Mapeo de Enums a nombres de archivos reales en el ZIP de Meta
        string fileNameToFind = type switch
        {
            RelationshipType.Followers => "followers_1.json",
            RelationshipType.Following => "following.json",
            RelationshipType.BlockedProfiles => "blocked_profiles.json",
            RelationshipType.CloseFriends => "close_friends.json",
            RelationshipType.FollowRequetsReceived => "follow_requests_received.json",
            RelationshipType.PendingFollowRequests => "pending_follow_requests.json",
            RelationshipType.ProfilesFavorited => "favorite_profiles.json",
            RelationshipType.RecentFollowRequests => "recent_follow_requests.json",
            RelationshipType.RecentlyUnfollowed => "recently_unfollowed_profiles.json",
            RelationshipType.RemovedSuggestions => "removed_suggestions.json",
            RelationshipType.RestrictedProfiles => "restricted_profiles.json",
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Tipo no soportado")
        };

        // 2. Localización del archivo dentro del ZIP
        var entry = archive.Entries.FirstOrDefault(e =>
            e.FullName.EndsWith(fileNameToFind, StringComparison.OrdinalIgnoreCase));

        if (entry == null) return profiles;

        // 3. Procesamiento del JSON
        using (var entryStream = entry.Open())
        {
            using var jsonDoc = await JsonDocument.ParseAsync(entryStream);

            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var element in jsonDoc.RootElement.EnumerateArray())
                {
                    ExtractFromNode(element, profiles);
                }
            }
            else if (jsonDoc.RootElement.ValueKind == JsonValueKind.Object)
            {
                // Buscamos dinámicamente cualquier propiedad que sea un array
                // Esto resuelve las llaves identificativas como "relationships_following"
                foreach (var property in jsonDoc.RootElement.EnumerateObject())
                {
                    if (property.Value.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var element in property.Value.EnumerateArray())
                        {
                            ExtractFromNode(element, profiles);
                        }
                    }
                }
            }
        }

        return profiles;
    }
    private void ExtractFromNode(JsonElement node, List<InstagramProfile> profiles)
    {
        string username = string.Empty;

        // En 'following', el nombre suele estar en el 'title'
        if (node.TryGetProperty("title", out var t))
            username = t.GetString() ?? "";

        if (node.TryGetProperty("string_list_data", out var dataList) && dataList.ValueKind == JsonValueKind.Array)
        {
            foreach (var data in dataList.EnumerateArray())
            {
                var currentUsername = username;

                // En 'followers', el nombre suele estar dentro de 'value'
                if (string.IsNullOrEmpty(currentUsername))
                {
                    if (data.TryGetProperty("value", out var v))
                        currentUsername = v.GetString() ?? "";
                }

                if (!string.IsNullOrEmpty(currentUsername))
                {
                    profiles.Add(new InstagramProfile
                    {
                        Username = currentUsername,
                        ProfileUrl = data.TryGetProperty("href", out var h) ? h.GetString() ?? "" : "",
                        OccurredAt = data.TryGetProperty("timestamp", out var ts)
                                     ? DateTimeOffset.FromUnixTimeSeconds(ts.GetInt64()).DateTime
                                     : DateTime.Now
                    });
                }
            }
        }
    }
}