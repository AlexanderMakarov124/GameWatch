using AutoMapper;
using GameWatch.Infrastructure;
using GameWatch.Tests.Utilities;
using GameWatch.UseCases.Games;
using GameWatch.UseCases.Games.Commands.CreateGame;
using GameWatch.UseCases.Games.Commands.DeleteGame;
using GameWatch.UseCases.Games.Commands.UpdateGame;
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