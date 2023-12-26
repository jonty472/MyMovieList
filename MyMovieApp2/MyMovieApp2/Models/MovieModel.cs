using Microsoft.EntityFrameworkCore;

namespace MyMovieApp2.Models;

public class RootObject
{
    public List<Movie>? results {get; set; }
}
public class Movie 
{
    public int MovieId {get; set;}
    public string? Title {get; set;}
    public ulong ReleaseDate {get; set;}
    public float Rating {get; set;}

    public ICollection<Watchlist> Watchlists {get; set;}
}