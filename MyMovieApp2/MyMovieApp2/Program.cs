using Microsoft.Extensions.Configuration;
using MyMovieApp2.Models;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        using (var myMovieList = new MyMovieListDbContext()) 
        {
            var movie = new Movie
            {
                Title = "Gladiator",
                ReleaseDate = 122,
                Rating = 8
            };

            var user = new User
            {
                Username = "Jim",
                RegisteredDate = (ulong)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
            };

            myMovieList.Movies.Add(movie);
            myMovieList.Users.Add(user);
            myMovieList.Watchlists.Add(new Watchlist {Movie = movie, User = user});
            myMovieList.SaveChanges();
        }

    }
}
