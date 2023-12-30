using Microsoft.AspNetCore.Mvc;
using MyMovieListApi.Models;

namespace MyMovieListApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieListController : ControllerBase
{
    private readonly MovieListApiDbContext _context;

    public MovieListController(MovieListApiDbContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<IActionResult> GetMovie(Movie movie)
    {
        return CreatedAtAction(actionName: "GetMovie", movie);
    }

    [HttpPost("{title}")]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(actionName: nameof(GetMovie), 
                               routeValues: new { title = movie.Title }, 
                               value: movie);
    }
}
