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
    
    public int GameListId { get; init; }
}
