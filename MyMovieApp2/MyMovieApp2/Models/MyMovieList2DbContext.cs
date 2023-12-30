using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyMovieApp2.Models;

public class MyMovieListDbContext: DbContext
{
    public DbSet<Movie> Movies {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<Watchlist> Watchlists {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyMovieList2; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL16.SQLEXPRESS\\MSSQL\\DATA\\MyMovieList2.mdf;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // // modelBuilder.Entity<Watchlist>()
        // //     .HasOne(watchlist => watchlist.Movie)
        // //     .WithMany(movie => movie.Watchlists)
        // //     .HasForeignKey(watchlist => watchlist.MovieId);

        // modelBuilder.Entity<Watchlist>()
        // .HasOne(watchlist => watchlist.Users)
        // .WithMany(user => user.Users)
        // .HasForeignKey(watchlist => watchlist.UserId);
    }
}