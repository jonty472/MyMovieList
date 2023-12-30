using Microsoft.EntityFrameworkCore;

namespace MyMovieListApi.Models;

public class MovieListApiDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public MovieListApiDbContext(DbContextOptions<MovieListApiDbContext> options) : base(options)
    {

    }
}