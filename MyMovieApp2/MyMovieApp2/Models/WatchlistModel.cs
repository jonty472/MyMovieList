namespace MyMovieApp2.Models;

public class Watchlist
{
    public int WatchlistId {get; set;}
    public int UserId {get; set;}
    public User User {get; set;}

    public int MovieId {get; set;}
    public Movie Movie {get; set;}
}