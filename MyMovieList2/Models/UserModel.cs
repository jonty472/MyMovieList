namespace MyMovieList.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public DateTime CreationDate { get; set; }

    public List<Movie>? Movies { get; set; }
    public List<Watchlist>? Watchlists { get; set; }
}