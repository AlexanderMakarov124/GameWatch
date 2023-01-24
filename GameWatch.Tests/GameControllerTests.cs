using AutoMapper;
using GameWatch.Backend.Controllers;
using GameWatch.Backend.MappingProfiles;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.Tests.Utilities;
using GameWatch.UseCases.DTOs;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameWatch.Tests;

public class GameControllerTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;
    private readonly ILogger<GameController> logger;
    private readonly IMapper mapper;

    public GameControllerTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
        logger = new Mock<ILogger<GameController>>().Object;

        var config = new MapperConfiguration(conf => conf.AddProfile(new GameMappingProfile()));
        mapper = config.CreateMapper();
    }

    [Fact]
    public void GetAllGames_Correct_CountShouldBe5()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameController(db, logger, mapper);

        const int expectedCount = 5;

        // Act
        var games = controller.GetAllGames();

        // Assert
        Assert.Equal(expectedCount, games.Count());
    }

    [Fact]
    public void GetGameByName_Correct_Success()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameController(db, logger, mapper);

        const int expectedId = 2;
        const string gameName = "Warcraft 3";

        // Act
        var game = controller.GetGameByName(gameName);

        // Assert
        Assert.Equal(expectedId, game.Id);
    }

    [Fact]
    public void GetGameByName_NameInDifferentCase_Success()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameController(db, logger, mapper);

        const int expectedId = 2;
        const string gameName = "warCRAFT 3";

        // Act
        var game = controller.GetGameByName(gameName);

        // Assert
        Assert.Equal(expectedId, game.Id);
    }

    [Fact]
    public void GetGameByName_GameDoesNotExist_ThrowNotFoundException()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameController(db, logger, mapper);

        const string gameName = "GameThatDoesNotExist";

        // Act
        var act = () => controller.GetGameByName(gameName);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Fact]
    public void CreateGame_Correct_GamesShouldContainNewGame()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameController(db, logger, mapper);

        var gameDto = new GameDto
        {
            Name = "NewGame",
            Genre = "NewGenre",
            GameListName = "Planned"
        };
        const string expectedGameList = "Planned";
        const string expectedName = "NewGame";

        // Act
        controller.CreateGameAsync(gameDto);

        db.ChangeTracker.Clear();

        // Assert

        var gameList = db.GameLists.Single(gl => gl.Name.ToLower().Equals(expectedGameList));

        db.Entry(gameList).Collection(gl => gl.Games).Load();

        var game = gameList.Games.Single(g => g.Name.Equals(expectedName));

        Assert.Equal(expectedName, game.Name);
    }

    [Fact]
    public void UpdateGame_Correct_GamesShouldContainUpdatedGame()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameController(db, logger, mapper);

        var updatedGame = db.Games.First();
        updatedGame.Name = "UpdatedGame";
        const string expectedName = "UpdatedGame";

        // Act
        controller.UpdateGame(updatedGame);

        db.ChangeTracker.Clear();

        // Assert
        var game = db.Games.Single(g => g.Name.Equals(expectedName));

        Assert.Equal(expectedName, game.Name);
    }

    [Fact]
    public void DeleteGame_Correct_GamesShouldNotContainDeletedGame()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameController(db, logger, mapper);

        var game = db.Games.First();

        // Act
        controller.DeleteGame(game.Name);

        db.ChangeTracker.Clear();

        // Assert
        Assert.DoesNotContain(game, db.Games);
    }
}