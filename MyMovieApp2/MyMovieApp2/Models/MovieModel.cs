using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyMovieApp2.Models;


public class searchResults
{
    public List<Movie>? Movies { get; set; }
}
public class Movie 
{
    public int MovieId {get; set;}
    public required string Title {get; set;}
    public required ulong ReleaseDate {get; set;}
    public float Rating {get; set;}
}