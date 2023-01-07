using GameWatch.Backend.Controllers;
using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.Tests;

public class UnitTests : IClassFixture<DatabaseFixture>
{
    public UnitTests(DatabaseFixture fixture)
    {
        Fixture = fixture;
    }
    public DatabaseFixture Fixture { get; }

    [Fact]
    public void Test1()
    {
        using var db = Fixture.CreateContext();
        var controller = new GameController(db);

        var game = controller.Get();

        Assert.Equal("Game1", game.Name);
    }
}

public class DatabaseFixture
{
    private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=GameWatchTests;Trusted_Connection=True";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public DatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.AddRange(
                        new Game { Name = "Game1" },
                        new Game { Name = "Game2" });
                    context.SaveChanges();
                }

                _databaseInitialized = true;
            }
        }
    }

    public ApplicationContext CreateContext()
        => new ApplicationContext(
            new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(ConnectionString)
                .Options);
}