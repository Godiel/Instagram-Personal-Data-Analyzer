using InstagramAnalyzer.Domain.Entities;
using InstagramAnalyzer.Domain.Enums;

namespace InstagramAnalyzer.Application.Interfaces;

public interface IInstagramService
{
    /// <summary>
    /// Procesa el archivo ZIP de Instagram y extrae los perfiles según el tipo de relación solicitado.
    /// </summary>
    /// <param name="zipStream">El flujo de datos del archivo .zip cargado.</param>
    /// <param name="type">El tipo de datos a extraer (Followers, Following, etc.).</param>
    /// <returns>Una lista de perfiles mapeados al dominio.</returns>
    Task<IEnumerable<InstagramProfile>> GetProfilesFromZipAsync(Stream zipStream, RelationshipType type);
}