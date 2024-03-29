﻿using System.ComponentModel.DataAnnotations;

namespace GameWatch.Domain.Entities;

/// <summary>
/// Game.
/// </summary>
public class Game
{
    /// <summary>
    /// Identifier.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Genres.
    /// </summary>
    public ICollection<Genre> Genres { get; set; } = new List<Genre>(500);

    /// <summary>
    /// User's description about game.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date when the game was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Game list which the game belongs to.
    /// </summary>
    public int GameListId { get; set; }

    /// <summary>
    /// Link to store with the game.
    /// </summary>
    public string? StoreLink { get; set; }

    /// <summary>
    /// Link to download the game.
    /// </summary>
    public string? DownloadLink { get; set; }

    /// <summary>
    /// Cover of the game.
    /// </summary>
    public string? CoverUrl { get; set; }

    /// <summary>
    /// Summary of the game.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Date when the game was firstly released.
    /// </summary>
    public DateTime? ReleaseDate { get; set; }
}
