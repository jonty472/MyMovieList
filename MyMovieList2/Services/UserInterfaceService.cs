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
        LoginMenu();

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
        while (!_userService.IsLoggedIn)
        {
            Console.WriteLine("1) Sign in\n2) Create account\n");
            switch(Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Attempting to login");
                    Login();
                    if (!_userService.IsLoggedIn)
                    {
                        string newUsersUsername = CreateUserAccount();
                        Login();
                    }
                    break;
                case "2":
                    CreateUserAccount();
                    Login();
                    break;
            }
        }
    }

    public void Login()
    {
        Console.WriteLine("========= Login ========");
        Console.Write("Useranme: ");
        string? username = Console.ReadLine();
        if (_userService.HasAccount(username))
        {
            _userService.IsLoggedIn = true;
        }
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