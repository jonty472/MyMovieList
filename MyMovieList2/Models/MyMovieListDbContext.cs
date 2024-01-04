using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyMovieList.Models;

public class MyMovieListDbContext : DbContext
{

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Watchlist> Watchlists {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
        .HasMany(entity => entity.Users)
        .WithMany(entity => entity.Movies)
        .UsingEntity<Watchlist>();

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.Property(entity => entity.UserRating)
                .IsRequired()
                .HasDefaultValue(0);
            entity.ToTable(table => table.HasCheckConstraint("CK_UserRating", "UserRating > 0 AND UserRating <= 10"));
                
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(entity => entity.Title).IsRequired();
            entity.HasIndex(entity => entity.Title).IsUnique();
            entity.Property(entity => entity.Year).IsRequired();
        });

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile($"{currentDirectory}/appsettings.json", true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        AppConfig appConfig = new AppConfig(config);
        string? connectionString = appConfig.ConnectionString;

        optionsBuilder.UseSqlServer(connectionString);
    }
}
