using GameWatch.Domain.Entities;

namespace GameWatch.UseCases.Games.Commands.CreateGame;

/// <summary>
/// DTO for creating <see cref="Game"/>
/// </summary>
public readonly record struct CreateGameDto(string Name, string GameListName);
