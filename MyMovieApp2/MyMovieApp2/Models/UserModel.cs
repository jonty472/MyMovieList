namespace MyMovieApp2.Models;

public class User
{
    public int UserId {get; set;}
    public string Username {get; set;}
    public ulong RegisteredDate {get; set;}
    public ICollection<Watchlist> Users {get; set;}
}