using AutoMapper;
using GameWatch.Backend.Controllers;
using GameWatch.Backend.MappingProfiles;
using GameWatch.Tests.Utilities;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameWatch.Tests;
public class GameListControllerTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;
    private readonly ILogger<GameListController> logger;
    private readonly IMapper mapper;

    public GameListControllerTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
        logger = new Mock<ILogger<GameListController>>().Object;

        var config = new MapperConfiguration(conf => conf.AddProfile(new GameListMappingProfile()));
        mapper = config.CreateMapper();

    }

    [Fact]
    public void GetAllGameLists_Correct_CountShouldBe3()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameListController(db, logger, mapper);

        const int expectedCount = 3;

        // Act
        var games = controller.GetAllGameLists();

        // Assert
        Assert.Equal(expectedCount, games.Count());
    }
}
