using MyMovieList.Models;
using MyMovieList.Services;

/*
MVP
- CRUD operations for movies, users, and watchlists
- display users watchlists
- use new API to populate movie fields (on re-factor use DI (dependecy injection) with IHttpClientFactory)
- constraints on rating e.g. 1-10
*/

MovieService movieService = new MovieService();
WatchlistService watchlistService = new WatchlistService();
UserService userService = new UserService();
