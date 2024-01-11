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
    
    private async Task<string> GetMovieAsync(string title)
    {
        return "";
    }
    private async Task<string> GetMovieAsync(string title, int releaseYear)
    {
        return "";
    }

    private Movie DeserializeMovieAsync(string jsonResponse)
    {
        return new Movie();
    }
    public void AddMovie(string title)
    {
        
    }
}