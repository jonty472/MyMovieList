using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{
    public class Watchlist
    {
        List<Movie> movies;
        public Watchlist()
        {
            movies = new List<Movie>();
        }

        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }
        public void GetMovies()
        {
            foreach(Movie movie in movies)
            {
                Console.WriteLine(movie.title);
            }
        }
    }
}
