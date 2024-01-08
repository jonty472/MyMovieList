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

    

}