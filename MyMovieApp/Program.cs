using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

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

            // lets treat program.cs like a form in delphi, but in this case the form is the console. 
            // I need to instatiate my classes on the form first MovieList, which will be created empty. Then create a movie where
            // we leave most of the properties blank or = null or load them as objects from the database

            Watchlist myList = new Watchlist();
            Movie movie = new Movie();
            string movieRequest = await movie.GetMovieAysnc(client, "oldboy");
            movie = movie.DeserializeMovieAsync(movieRequest);

            myList.AddMovie(movie);

            myList.GetMovies();

        }
    }
}
