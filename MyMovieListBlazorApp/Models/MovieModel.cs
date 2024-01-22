using System.Text.Json.Serialization;

namespace MyMovieListBlazorApp
{
    public class OMDbResponse
{
    [JsonPropertyName("Search")]
    public List<Movie>? Response { get; set; }
}


public class Movie
{
    public string? Title { get; set; }

    public int Year { get; set; }
}
}


