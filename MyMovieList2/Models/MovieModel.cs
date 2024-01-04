using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyMovieList.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public int IMDbVotes { get; set; }
    public decimal IMDbRating { get; set; }

    public List<User>? Users { get; set; }
    public List<Watchlist>? Watchlists { get; set; }
}