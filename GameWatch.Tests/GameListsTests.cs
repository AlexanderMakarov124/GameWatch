using AutoMapper;
using GameWatch.Backend.MappingProfiles;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.Tests.Utilities;
using GameWatch.UseCases.DTOs;
using GameWatch.UseCases.GameLists.CreateGameList;
using GameWatch.UseCases.GameLists.DeleteGameList;
using GameWatch.UseCases.GameLists.GetAllGameLists;
using GameWatch.UseCases.GameLists.GetGameListById;
using GameWatch.UseCases.GameLists.UpdateGameList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameWatch.Tests;
public class GameListsTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;
    private readonly IMapper mapper;

    public GameListsTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;

        var config = new MapperConfiguration(conf => conf.AddProfile(new GameListMappingProfile()));
        mapper = config.CreateMapper();
    }

    [Fact]
    public async void GetAllGameLists_Correct_CountShouldBe3()
    {
        // Arrange
        await using var db = fixture.CreateContext();

        var query = new GetAllGameListsQuery();

        var logger = new Mock<ILogger<GetAllGameListsQueryHandler>>().Object;
        var handler = new GetAllGameListsQueryHandler(db, logger);

        const int expectedCount = 3;

        // Act
        var games = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedCount, games.Count());
    }

    [Fact]
    public async void GetGameListById_Correct_Success()
    {
        // Arrange
        await using var db = fixture.CreateContext();

        const int id = 2;

        var query = new GetGameListByIdQuery
        {
            Id = id
        };

        var logger = new Mock<ILogger<GetGameListByIdQueryHandler>>().Object;
        var handler = new GetGameListByIdQueryHandler(db, logger);

        const string expectedName = "Played";

        // Act
        var game = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedName, game.Name);
    }

    [Fact]
    public async void GetGameListById_GivenIdDoesNotExist_ThrowNotFoundException()
    {
        // Arrange
        await using var db = fixture.CreateContext();

        const int id = 0;

        var query = new GetGameListByIdQuery
        {
            Id = id
        };

        var logger = new Mock<ILogger<GetGameListByIdQueryHandler>>().Object;
        var handler = new GetGameListByIdQueryHandler(db, logger);

        // Act
        var act = async () => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void GetGameListById_IsGamesLoaded_True()
    {
        // Arrange
        await using var db = fixture.CreateContext();

        const int id = 2;

        var query = new GetGameListByIdQuery
        {
            Id = id
        };

        var logger = new Mock<ILogger<GetGameListByIdQueryHandler>>().Object;
        var handler = new GetGameListByIdQueryHandler(db, logger);

        // Act
        var game = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(game.Games);
    }

    [Fact]
    public async void CreateGameList_Correct_Success()
    {
        // Arrange
        await using var db = fixture.CreateContext();
        await db.Database.BeginTransactionAsync();

        var gameListDto = new GameListDto
        {
            Name = "TestGameList"
        };

        var command = new CreateGameListCommand
        {
            GameListDto = gameListDto
        };

        var logger = new Mock<ILogger<CreateGameListCommandHandler>>().Object;
        var handler = new CreateGameListCommandHandler(db, logger, mapper);

        const int expectedId = 4;
        const string expectedName = "TestGameList";

        // Act
        await handler.Handle(command, CancellationToken.None);

        db.ChangeTracker.Clear();

        // Assert
        var gameList = await db.GameLists.FirstAsync(gl => gl.Id == expectedId);

        Assert.Equal(expectedName, gameList.Name);
    }

    [Fact]
    public async void CreateGameList_GameListAlreadyExist_ThrowAlreadyExistException()
    {
        // Arrange
        await using var db = fixture.CreateContext();
        await db.Database.BeginTransactionAsync();

        var gameListDto = new GameListDto
        {
            Name = "Favorite"
        };

        var command = new CreateGameListCommand
        {
            GameListDto = gameListDto
        };

        var logger = new Mock<ILogger<CreateGameListCommandHandler>>().Object;
        var handler = new CreateGameListCommandHandler(db, logger, mapper);

        // Act
        var act = async () => await handler.Handle(command, CancellationToken.None);

        db.ChangeTracker.Clear();

        // Assert
        await Assert.ThrowsAsync<AlreadyExistException>(act);
    }

    [Fact]
    public async void UpdateGameList_Correct_Success()
    {
        // Arrange
        await using var db = fixture.CreateContext();
        await db.Database.BeginTransactionAsync();

        const string newName = "Planned with new PC";
        const int id = 3;

        var gameList = await db.GameLists.FirstAsync(gl => gl.Id == id);

        gameList.Name = newName;

        var command = new UpdateGameListCommand
        {
            GameList = gameList
        };

        var logger = new Mock<ILogger<UpdateGameListCommandHandler>>().Object;
        var handler = new UpdateGameListCommandHandler(db, logger);

        const string expectedName = newName;

        // Act
        await handler.Handle(command, CancellationToken.None);

        db.ChangeTracker.Clear();

        // Assert
        var updatedGameList = await db.GameLists.FirstAsync(gl => gl.Id == id);

        Assert.Equal(expectedName, updatedGameList.Name);
    }

    [Fact]
    public async void UpdateGameList_GameListAlreadyExist_ThrowAlreadyExistException()
    {
        // Arrange
        await using var db = fixture.CreateContext();
        await db.Database.BeginTransactionAsync();

        const string newName = "Played";
        const int id = 3;

        var gameList = await db.GameLists.FirstAsync(gl => gl.Id == id);

        gameList.Name = newName;

        var command = new UpdateGameListCommand
        {
            GameList = gameList
        };

        var logger = new Mock<ILogger<UpdateGameListCommandHandler>>().Object;
        var handler = new UpdateGameListCommandHandler(db, logger);

        // Act
        var act = async () => await handler.Handle(command, CancellationToken.None);

        db.ChangeTracker.Clear();

        // Assert
        await Assert.ThrowsAsync<AlreadyExistException>(act);
    }

    [Fact]
    public async void DeleteGameList_Correct_Success()
    {
        // Arrange
        await using var db = fixture.CreateContext();
        await db.Database.BeginTransactionAsync();

        const int id = 1;

        var command = new DeleteGameListCommand
        {
            Id = id
        };

        var logger = new Mock<ILogger<DeleteGameListCommandHandler>>().Object;
        var handler = new DeleteGameListCommandHandler(db, logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        db.ChangeTracker.Clear();

        // Assert
        var gameList = db.GameLists.FirstOrDefault(gl => gl.Id == id);

        Assert.Null(gameList);
    }
}
