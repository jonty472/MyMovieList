using MyMovieList.Models;

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
    public void MainMenuUserInterface()
    {
        // display login menu first
        // once logged in show main menu
    
        _isMainMenuDisplayed = true;
        Console.WriteLine("1) Create an account\n" +
                          "2) Add a movie\n" +
                          "3) Remove a movie\n" +
                          "4) View watchlist");   
        while (_isMainMenuDisplayed)
        {
            switch(Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("test login");
                    _isMainMenuDisplayed = false;
                    break;
                case "2":
                    Console.WriteLine("test movie search");
                    _isMainMenuDisplayed = false;
                    break;

            }
        }
    }

    public void LoginMenu()
    {

    }

    public void AddMovieMenu()
    {

    }


    public string CreateUserAccount()
    {
        Console.Write("====Account Creation ====\n" +
                           "Username? ");
        string? username = Console.ReadLine();
        _userService.AddUser(username);
        return username;
    }

    public DateTime GetUserCreationDate(string username)
    {
        return _userService.GetUserCreationDate(username);
    }

    
}