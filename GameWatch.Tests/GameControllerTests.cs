using GameWatch.Backend.Controllers;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common;
using GameWatch.Tests.Utilities;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameWatch.Tests;

public class GameControllerTests : IClassFixture<DatabaseFixture>
{
    public GameControllerTests(DatabaseFixture fixture)
    {
        Fixture = fixture;
    }
    public DatabaseFixture Fixture { get; }

    [Fact]
    public void GetAllGames_Correct_CountShouldBe2()
    {
        // Arrange
        using var db = Fixture.CreateContext();
        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

        const int expectedCount = 2;

        // Act
        var games = controller.GetAllGames();

        // Assert
        Assert.Equal(expectedCount, games.Count());
    }

    [Fact]
    public void GetGameByName_Correct_Success()
    {
        // Arrange
        using var db = Fixture.CreateContext();
        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

        const int expectedId = 1;
        const string gameName = "Game1";

        // Act
        var game = controller.GetGameByName(gameName);

        // Assert
        Assert.Equal(expectedId, game.Id);
    }

    [Fact]
    public void GetGameByName_NameInDifferentCase_Success()
    {
        // Arrange
        using var db = Fixture.CreateContext();
        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

        const int expectedId = 1;
        const string gameName = "gAmE1";

        // Act
        var game = controller.GetGameByName(gameName);

        // Assert
        Assert.Equal(expectedId, game.Id);
    }

    [Fact]
    public void GetGameByName_GameDoesNotExist_ThrowNotFoundException()
    {
        // Arrange
        using var db = Fixture.CreateContext();
        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

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
        using var db = Fixture.CreateContext();
        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

        var game = new Game
        {
            Name = "NewGame"
        };
        const int expectedCount = 3;

        // Act
        controller.CreateGame(game);

        // Assert
        Assert.Contains(game, db.Games);
        Assert.Equal(expectedCount, db.Games.Count());
    }
}