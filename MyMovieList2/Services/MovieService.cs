using MyMovieList.Models;

namespace MyMovieList.Services;

public class MovieService : BaseService
{
    // use MyMovieListDbContext here for the UI logic (e.g. don't need to re-created the movie properties)
    public int Test()
    {
        var test1 = new BaseService();
        return test1.GetValue();
    }

    public void Test2()
    {
        var title = _context.Movies.Where(movie => movie.Title.StartsWith("G"));
    }

    public void AddMovie(string title)
    {

    }
}