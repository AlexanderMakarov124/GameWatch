using GameWatch.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.Tests.Utilities;

/// <summary>
/// Database fixture that imitates the database.
/// </summary>
public class DatabaseFixture
{
    private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=GameWatchTests;Trusted_Connection=True;";

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

                    const string queriesPath = "../../../../GameWatch.DataAccess/Queries";

                    var sqlQueries = new List<string>
                    {
                        //File.ReadAllText($"{queriesPath}/CREATE/CreateGameLists.sql"),
                        //File.ReadAllText($"{queriesPath}/CREATE/CreateGames.sql"),

                        File.ReadAllText($"{queriesPath}/INSERT/InsertGameLists.sql"),
                        File.ReadAllText($"{queriesPath}/INSERT/InsertGames.sql")
                    };
                    
                    foreach (var sql in sqlQueries)
                    {
                        context.Database.ExecuteSqlRaw(sql);
                    }

                    //context.Database.ExecuteSql($"EXECUTE CreateGameLists");
                    //context.Database.ExecuteSql($"EXECUTE CreateGames");

                    context.Database.ExecuteSql($"EXECUTE InsertGameLists");
                    context.Database.ExecuteSql($"EXECUTE InsertGames");


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
