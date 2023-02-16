using GameWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.DataAccess;

/// <summary>
/// Database application context.
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    /// <summary>
    /// Games DB set.
    /// </summary>
    public DbSet<Game> Games { get; protected set; }
    
    /// <summary>
    /// GameLists DB set.
    /// </summary>
    public DbSet<GameList> GameLists { get; protected set; }

    /// <summary>
    /// Genres DB set.
    /// </summary>
    public DbSet<Genre> Genres { get; protected set; }
}
