using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.Tests.Utilities;
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

    public ApplicationContext CreateContext() => new ApplicationContext(
            new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(ConnectionString)
                .Options);
}
