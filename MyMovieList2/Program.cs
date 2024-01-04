using MyMovieList.Models;

/*
MVP
- CRUD operations for movies, users, and watchlists
- display users watchlists
- use new API to populate movie fields
- constraints on rating e.g. 1-10
*/
MyMovieListDbContext dbContext = new MyMovieListDbContext();
Movie movie = new Movie()
{
    Title = "Test",
    Year = 2023,
    IMDbVotes = 1,
    IMDbRating = 10,
};

dbContext.Add(movie);
dbContext.SaveChanges();