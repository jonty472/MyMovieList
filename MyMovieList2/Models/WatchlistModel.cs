namespace MyMovieList.Models;

public class Watchlist
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public decimal UserRating { get; set; }
}