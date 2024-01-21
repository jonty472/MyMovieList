using Microsoft.IdentityModel.Tokens;
using MyMovieList.Models;
using Newtonsoft.Json.Bson;

namespace MyMovieList.Services;

public class UserInterfaceService
{
    private Movie? _movie;
    private User? _user;
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

    public async Task UserInterface()
    {
        bool showMainMenu = true;
        // LoginMenu();
        // await AddMovie();

        while(showMainMenu)
        {
            Console.WriteLine(
                "1) Add movie to MyMovieList\n"
                // "2) Edit MyMovieList\n" +
                // "3) test\n"
            );

            switch(Console.ReadLine())
            {
                case "1":
                    Movie movie = await _movieService.GetMovieFromDbAsync("oldboy");
                    if (movie.Title.IsNullOrEmpty())
                    {
                        Console.WriteLine("movie doesn't exist");
                    }
                    else
                    {
                        _movie = movie;
                    }
                    break;
                case "2":
                    break;
                case "3":
                    break;
            }
        }
    }

    private void LoginMenu()
    {
        Console.Clear();
        while(!_userService.IsLoggedIn)
        {
            Console.WriteLine("1) Login\n" +
                              "2) Create an account");
            
            switch(Console.ReadLine())
            {
                case "1":
                    Login();
                    break;
            }
        }
    }

    private void Login()
    {   string? username = "";
        Console.Write("Username: ");
        username = Console.ReadLine();
        if (_userService.HasAccount(username))
        {
            Console.Write("Sucessfully logged in.");
            _user = _userService.GetUser(username);
            _userService.IsLoggedIn = true;
        }
        else
        {
            Console.WriteLine("Account doesn't exist.");
        }
    }

    private void EditMovieListMenu()
    {
    }
    private async Task AddMovie(string title)
    {
        List<Movie> movies = await _movieService.GetMovieAsync(title);

        foreach (var movie in movies)
        {
            if(CorrectMovie(movie))
            {
                await _movieService.AddMovie(movie);
                Console.WriteLine("Movie added.");
            }
        }
    }

    private async Task<Movie> GetMovie(string title)
    {
        return await _movieService.GetMovieFromDbAsync(title);
    }

    private void AddMovieToWatchlist()
    {
        if (_user == null || _movie == null)
        {
            throw new Exception("user or movie doesn't exist");
        }
        _watchlistService.AddMovieToWatchlist(_user, _movie);
    }

    private bool CorrectMovie(Movie movie)
    {
        bool correctMovie = false;
        while (!correctMovie)
        {
            Console.Write($"{movie.Title}, {movie.Year}\nRight Movie? ");
            string? response = Console.ReadLine();
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