namespace MyMovieListApi.Models;

public class Movie
{
    public long MovieId { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ReWatches { get; set ;}
}