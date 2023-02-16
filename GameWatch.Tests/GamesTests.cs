using AutoMapper;
using GameWatch.Backend.MappingProfiles;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.Tests.Utilities;
using GameWatch.UseCases.DTOs;
using GameWatch.UseCases.Games.CreateGame;
using GameWatch.UseCases.Games.DeleteGame;
using GameWatch.UseCases.Games.GetGamesByName;
using GameWatch.UseCases.Games.UpdateGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameWatch.Tests;

public class GamesTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;
    private readonly IMapper mapper;

    public GamesTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;

        var config = new MapperConfiguration(conf => conf.AddProfile(new GameMappingProfile()));
        mapper = config.CreateMapper();
    }

    //[Fact]
    //public async void CreateGame_Correct_GamesShouldContainNewGame()
    //{
    //    // Arrange
    //    await using var db = fixture.CreateContext();
    //    await db.Database.BeginTransactionAsync();

    //    var gameDto = new GameDto
    //    {
    //        Name = "NewGame",
    //        Genre = "NewGenre",
    //        GameListName = "Planned"
    //    };

    //    var command = new CreateGameCommand
    //    {
    //        GameDto = gameDto
    //    };

    //    var logger = new Mock<ILogger<CreateGameCommandHandler>>().Object;
    //    var handler = new CreateGameCommandHandler(db, logger, mapper);

    //    const string expectedGameList = "Planned";
    //    const string expectedName = "NewGame";

    //    // Act
    //    await handler.Handle(command, CancellationToken.None);

    //    db.ChangeTracker.Clear();

    //    // Assert
    //    var gameList = await db.GameLists.SingleAsync(gl => gl.Name.ToLower().Equals(expectedGameList));

    //    await db.Entry(gameList).Collection(gl => gl.Games).LoadAsync();

    //    var game = gameList.Games.Single(g => g.Name.Equals(expectedName));

    //    Assert.Equal(expectedName, game.Name);
    //}

    //[Fact]
    //public async void CreateGame_GameListDoesNotExist_ThrowNotFoundException()
    //{
    //    // Arrange
    //    await using var db = fixture.CreateContext();
    //    await db.Database.BeginTransactionAsync();

    //    var gameDto = new GameDto
    //    {
    //        Name = "NewGame",
    //        Genre = "NewGenre",
    //        GameListName = "UnknownName"
    //    };

    //    var command = new CreateGameCommand
    //    {
    //        GameDto = gameDto
    //    };

    //    var logger = new Mock<ILogger<CreateGameCommandHandler>>().Object;
    //    var handler = new CreateGameCommandHandler(db, logger, mapper);

    //    // Act
    //    var act = async () => await handler.Handle(command, CancellationToken.None);

    //    db.ChangeTracker.Clear();

    //    // Assert
    //    await Assert.ThrowsAsync<NotFoundException>(act);
    //}

    [Fact]
    public async void GetGamesByName_Correct_GamesCountShouldBe2AndShouldContainStarcraft2()
    {
        // Arrange
        await using var db = fixture.CreateContext();

        const string name = "craft";

        var query = new GetGamesByNameQuery
        {
            Name = name
        };
        
        var handler = new GetGamesByNameQueryHandler(db);

        const int expectedCount = 2;
        const string expectedName = "Starcraft 2";

        // Act
        var games = await handler.Handle(query, CancellationToken.None);
        games = games.ToList();

        // Assert
        Assert.Equal(expectedCount, games.Count());
        Assert.Contains(games, g => g.Name.Equals(expectedName));
    }

    [Fact]
    public async void UpdateGame_Correct_GamesShouldContainUpdatedGame()
    {
        // Arrange
        await using var db = fixture.CreateContext();
        await db.Database.BeginTransactionAsync();

        var updatedGame = db.Games.First();
        updatedGame.Name = "UpdatedGame";

        var command = new UpdateGameCommand
        {
            Game = updatedGame
        };

        var logger = new Mock<ILogger<UpdateGameCommandHandler>>().Object;
        var handler = new UpdateGameCommandHandler(db, logger);

        const string expectedName = "UpdatedGame";

        // Act
        await handler.Handle(command, CancellationToken.None);

        db.ChangeTracker.Clear();

        // Assert
        var game = await db.Games.SingleAsync(g => g.Name.Equals(expectedName));

        Assert.Equal(expectedName, game.Name);
    }

    [Fact]
    public async void DeleteGame_Correct_GamesShouldNotContainDeletedGame()
    {
        // Arrange
        await using var db = fixture.CreateContext();
        await db.Database.BeginTransactionAsync();

        var name = db.Games.First().Name;

        var command = new DeleteGameCommand
        {
            Name = name
        };

        var logger = new Mock<ILogger<DeleteGameCommandHandler>>().Object;
        var handler = new DeleteGameCommandHandler(db, logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        db.ChangeTracker.Clear();

        // Assert
        var game = await db.Games.FirstOrDefaultAsync(g => g.Name.ToLower().Equals(name.ToLower()));

        Assert.Null(game);
    }
}