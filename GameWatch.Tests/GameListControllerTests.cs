using GameWatch.Backend.Controllers;
using GameWatch.Tests.Utilities;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameWatch.Tests;
public class GameListControllerTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;

    public GameListControllerTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void GetAllGameLists_Correct_CountShouldBe3()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var mockLogger = new Mock<ILogger<GameController>>();
        var controller = new GameListController(db, mockLogger.Object);

        const int expectedCount = 3;

        // Act
        var games = controller.GetAllGameLists();

        // Assert
        Assert.Equal(expectedCount, games.Count());
    }
}
