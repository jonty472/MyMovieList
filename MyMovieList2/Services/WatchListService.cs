using MyMovieList.Models;

namespace MyMovieList.Services;

public class WatchlistService : BaseService
{
    public List<Movie> GetWatchlist(string username)
    {
        // _context.Watchlists.ToList(watchlist => watchlist.Movie)
        return new List<Movie>();
    }
}