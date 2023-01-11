using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.Tests.Utilities;

/// <summary>
/// Database fixture that imitates the database.
/// </summary>
public class DatabaseFixture
{
    private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=GameWatchTests;Trusted_Connection=True";

    private static readonly object Lock = new();
    private static bool databaseInitialized;

    /// <summary>
    /// Constructor that initializes the database.
    /// </summary>
    public DatabaseFixture()
    {
        lock (Lock)
        {
            if (!databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.Database.ExecuteSqlRaw(
                        "INSERT INTO Games (Name, Genre, Description, CreatedAt)" +
                        "VALUES " +
                        "('Starcraft 2', 'RTS', 'Best', GETDATE()), " +
                        "('Warcraft 3', 'RTS', 'Best', GETDATE()), " +
                        "('League of Legends', 'MOBA', 'Best MOBA', GETDATE()), " +
                        "('Dota 2', 'MOBA', '....', GETDATE());");
                    
                    context.SaveChanges();
                }

                databaseInitialized = true;
            }
        }
    }

    /// <summary>
    /// Creates context.
    /// </summary>
    /// <returns>Context.</returns>
    public ApplicationContext CreateContext() => new(
        new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(ConnectionString)
            .Options);

}
