namespace GameWatch.Domain.Entities;

/// <summary>
/// Genre of the game.
/// </summary>
public class Genre
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; init; }

    public ICollection<Game> Games { get; init; } = new List<Game>(500);
}
