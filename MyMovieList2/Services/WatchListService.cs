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
            UserRating = 9
        };
        _context.Watchlists.Add(watchlist);
        _context.SaveChanges();
    }
}