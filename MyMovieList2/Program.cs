using MyMovieList.Models;
using MyMovieList.Services;

/*
MVP
- CRUD operations for movies, users, and watchlists
- display users watchlists
- use new API to populate movie fields (on re-factor use DI (dependecy injection) with IHttpClientFactory)
- constraints on rating e.g. 1-10
- come up with lots of methods that require ef queries
    get users settings
    get movies rated above a given rating
    
*/

BaseService baseService = new BaseService();

MovieService movieService = new MovieService();

WatchlistService watchlistService = new WatchlistService();

UserService userService = new UserService();

UserInterfaceService userInterfaceService = new UserInterfaceService(movieService, watchlistService, userService);

await userInterfaceService.UserInterface();