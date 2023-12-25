using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyMovieApp2.Models;

public class MyMovieListDbContext: DbContext
{
    public DbSet<Movie> Movie {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("MyMovieListDbContext"));
}