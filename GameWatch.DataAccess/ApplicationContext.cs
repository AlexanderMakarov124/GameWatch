using System;
using System.Collections.Generic;
using GameWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3214EC074ED0C736");

            entity.Property(e => e.Name)
                .HasMaxLength(75)
                .IsUnicode(false);
        });
    }
}
