namespace GameWatch.Domain.Entities;

/// <summary>
/// Game.
/// </summary>
public class Game
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Genre.
    /// </summary>
    public string Genre { get; set; } = null!;

    /// <summary>
    /// User's description about game.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date when the game was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Game list which this game belongs to.
    /// </summary>
    public int GameListId { get; init; }

    /// <summary>
    /// Link to store with this game.
    /// </summary>
    public string? StoreLink { get; init; }

    /// <summary>
    /// Link to download this game.
    /// </summary>
    public string? DownloadLink { get; init; }
}
