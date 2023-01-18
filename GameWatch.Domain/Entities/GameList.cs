namespace GameWatch.Domain.Entities;

/// <summary>
/// List that contain games.
/// </summary>
public class GameList
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name of the list.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Games related to the game list.
    /// </summary>
    public ICollection<Game> Games { get; init; } = null!;
}
