namespace MyMovieApp2.Models;

public class RootObject
{
    public List<Movie>? results {get; set; }
}
public class Movie 
{
    private int Id {get; set;}
    private string? Title {get; set;}
    private DateTime ReleaseDate {get; set;}
    private decimal Rating {get; set;}

}