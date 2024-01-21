using Microsoft.EntityFrameworkCore;
using MyMovieList.Models;

namespace MyMovieList.Services;

public class WatchlistService : BaseService
{
    public List<Movie> GetWatchlist(string username)
    {
        // _context.Watchlists.ToList(watchlist => watchlist.Movie)
        return new List<Movie>();
    }

    public void AddMovieToWatchlist(User user, Movie movie)
    {
        Watchlist watchlist = new Watchlist()
        {
            MovieId = movie.Id,
            UserId = user.Id,
        };
        _context.Watchlists.Add(watchlist);
        _context.SaveChanges();
    }

    public void GetWatchlist(User user)
    {
        int targetUserId = user.Id;
        var usersMovies = (from u in _context.Users
                          join watchlist in _context.Watchlists on user.Id equals watchlist.UserId
                          join movie in _context.Movies on watchlist.MovieId equals movie.Id
                          where user.Id == targetUserId
                          select new 
                          {
                            MovieId = movie.Id,
                            Title = movie.Title
                          }).Distinct();

        foreach (var userMovie in usersMovies)
        {
            Console.Write($"test {userMovie.Title}");
        }
    }
}