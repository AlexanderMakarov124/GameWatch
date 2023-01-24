using AutoMapper;
using GameWatch.Backend.Controllers;
using GameWatch.Backend.MappingProfiles;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.Tests.Utilities;
using GameWatch.UseCases.DTOs;
using Microsoft.AspNetCore.Mvc;
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

    [Fact]
    public void GetGameListById_Correct_Success()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameListController(db, logger, mapper);

        const int id = 2;
        const string expectedName = "Played";

        // Act
        var game = controller.GetGameListById(id);

        // Assert
        Assert.Equal(expectedName, game.Name);
    }

    [Fact]
    public void GetGameListById_GivenIdDoesNotExist_ThrowNotFoundException()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameListController(db, logger, mapper);

        const int id = 0;

        // Act
        var act = () => controller.GetGameListById(id);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Fact]
    public void GetGameListById_IsGamesLoaded_True()
    {
        // Arrange
        using var db = fixture.CreateContext();
        var controller = new GameListController(db, logger, mapper);

        const int id = 2;

        // Act
        var game = controller.GetGameListById(id);

        // Assert
        Assert.NotNull(game.Games);
    }

    [Fact]
    public void CreateGameList_Correct_Success()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameListController(db, logger, mapper);

        var gameListDto = new GameListDto
        {
            Name = "TestGameList"
        };

        const int expectedId = 4;
        const string expectedName = "TestGameList";

        // Act
        var result = controller.CreateGameList(gameListDto);

        db.ChangeTracker.Clear();

        // Assert
        var game = controller.GetGameListById(expectedId);

        Assert.Equal(expectedName, game.Name);
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void CreateGameList_GameListAlreadyExist_ThrowAlreadyExistException()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameListController(db, logger, mapper);

        var gameListDto = new GameListDto
        {
            Name = "Favorite"
        };

        // Act
        var act = () => controller.CreateGameList(gameListDto);

        db.ChangeTracker.Clear();

        // Assert
        Assert.Throws<AlreadyExistException>(act);
    }

    [Fact]
    public void UpdateGameList_Correct_Success()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameListController(db, logger, mapper);

        const string newName = "Planned with new PC";
        const int id = 3;

        var gameList = controller.GetGameListById(id);

        gameList.Name = newName;

        const string expectedName = newName;

        // Act
        controller.UpdateGameList(gameList);

        db.ChangeTracker.Clear();

        // Assert
        var updatedGameList = controller.GetGameListById(id);

        Assert.Equal(expectedName, updatedGameList.Name);
    }

    [Fact]
    public void UpdateGameList_GameListAlreadyExist_ThrowAlreadyExistException()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameListController(db, logger, mapper);

        const string newName = "Played";
        const int id = 3;

        var gameList = controller.GetGameListById(id);

        gameList.Name = newName;

        // Act
        var act = () => controller.UpdateGameList(gameList);

        db.ChangeTracker.Clear();

        // Assert
        Assert.Throws<AlreadyExistException>(act);
    }

    [Fact]
    public void DeleteGameList_Correct_Success()
    {
        // Arrange
        using var db = fixture.CreateContext();
        db.Database.BeginTransaction();

        var controller = new GameListController(db, logger, mapper);

        const int id = 1;

        // Act
        controller.DeleteGameList(id);

        db.ChangeTracker.Clear();

        // Assert
        var gameList = db.GameLists.FirstOrDefault(gl => gl.Id == id);

        Assert.Null(gameList);
    }
}
