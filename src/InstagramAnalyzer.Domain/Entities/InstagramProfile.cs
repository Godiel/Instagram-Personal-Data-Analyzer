namespace InstagramAnalyzer.Domain.Entities;

/// <summary>
/// Representa a un usuario de Instagram extraído de los datos de Meta.
/// </summary>
public class InstagramProfile
{
    // El 'value' en el JSON de Instagram
    public string Username { get; init; } = string.Empty;

    // El 'href' en el JSON
    public string ProfileUrl { get; init; } = string.Empty;

    // El 'timestamp' convertido a objeto DateTime de C#
    public DateTime OccurredAt { get; init; }

    // Propiedad de solo lectura para facilitar la lógica en la UI
    public string DisplayName => $"@{Username}";

    // Propiedad de solo lectura para obtener el link
    public string InstagramLink => $"https://www.instagram.com/{Username}";

    // Lógica de dominio: Instagram a veces manda perfiles vacíos o erróneos
    public bool IsValid => !string.IsNullOrWhiteSpace(Username);
}