using MyMovieList.Models;

namespace MyMovieList.Services;

public class UserInterfaceService
{
    private MovieService _movieService;
    private WatchlistService _watchlistService;
    private UserService _userService;
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