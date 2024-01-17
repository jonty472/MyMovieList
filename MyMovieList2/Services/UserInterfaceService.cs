using Microsoft.IdentityModel.Tokens;
using MyMovieList.Models;
using Newtonsoft.Json.Bson;

namespace MyMovieList.Services;

public class UserInterfaceService
{
    private MovieService _movieService;
    private WatchlistService _watchlistService;
    private UserService _userService;
    private bool _isMainMenuDisplayed;
    public UserInterfaceService(
                                MovieService movieService,
                                WatchlistService watchlistService,
                                UserService userService
                                )
    {
        this._movieService = movieService;
        this._watchlistService = watchlistService;
        this._userService = userService;
    }

    public async Task MainMenuUserInterface()
    {
        // login menu
        // add movie menu
        await AddMovie();
    }

    private void LoginMenu(User user)
    {

    }

    private void Login()
    {

    }

    private async Task AddMovie()
    {
        List<Movie> movies = await _movieService.GetMovieAsync("Gladiator");

        foreach (var movie in movies)
        {
            if(CorrectMovie(movie))
            {
                break;
            }
        }
    }

    private bool CorrectMovie(Movie movie)
    {
        bool correctMovie = false;
        while (!correctMovie)
        {
            string? response = "";
            Console.Write($"{movie.Title}, {movie.Year}\nRight Movie? ");
            response = Console.ReadLine();
            if (response == "Y" || response == "y")
            {
                correctMovie = true;
                return true;
            }
            else if (response == "N" || response == "n")
            {
                return false;
            }
        }
        return correctMovie;
    }
    
}