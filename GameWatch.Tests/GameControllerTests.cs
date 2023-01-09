using GameWatch.Backend.Controllers;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common;
using GameWatch.Tests.Utilities;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameWatch.Tests;

public class GameControllerTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;

    public GameControllerTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void GetAllGames_Correct_CountShouldBe2()
    {
        // Arrange
        using var db = fixture.CreateContext();
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
        using var db = fixture.CreateContext();
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
        using var db = fixture.CreateContext();
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
        using var db = fixture.CreateContext();
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
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

        var newGame = new Game
        {
            Name = "NewGame"
        };
        const string expectedName = "NewGame";

        // Act
        controller.CreateGame(newGame);

        db.ChangeTracker.Clear();

        // Assert
        var game = db.Games.Single(g => g.Name.Equals(expectedName));

        Assert.Equal(expectedName, game.Name);
    }

    [Fact]
    public void UpdateGame_Correct_GamesShouldContainUpdatedGame()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

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

        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameController(db, mockLogger.Object);

        var game = db.Games.First();

        // Act
        controller.DeleteGame(game);

        db.ChangeTracker.Clear();

        // Assert
        Assert.DoesNotContain(game, db.Games);
    }
}