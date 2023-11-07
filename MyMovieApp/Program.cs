using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using MyMovieApp.Watchlist;

namespace MyMovieApp
{
    public class Program
    {

        /*
         * MVP -
         * GET request movie(s)
         * Deserialize movie GET request into a Movie object
         * Choose the correct movie to add to database
         * Have different users that hae their own movie list
         */

        /// <summary>
        /// HttpClient is intended to be instantiated once per application, rather than per-use.
        /// This is done to reduce the chances of a socket error
        /// </summary>
        public static readonly HttpClient client = new HttpClient();

        public static readonly string connectionString = "Server=DESKTOP-7O5A39Q\\SQLEXPRESS ;Integrated Security=true; Database=MovieDatabase;";
        static async Task Main(string[] args)
        {
            
            MovieList myList = new Watchlist.MovieList();
            
            //Movie movie = await myList.GetMovieAysnc(client, "Gladiator", 2000);
            Movie movie2 = await myList.GetMovieAysnc(client, "Lion King", 1994);
            Movie movie3 = new Movie() { id = 98, title = "Gladiator", release_date = "2000-05-04" };

            Movie movie4 = new Movie() { id = 670, title = "Oldboy", release_date = "2003-11-21" };
            myList.AddMovie(movie2);
            myList.AddMovie(movie3);
            myList.AddMovie(movie4);
            myList.SetMovieListRating();
            myList.GetMovieListRatings();
            myList.SortByUserRating();
            myList.DisplayMovieList();
        }
    }
}
