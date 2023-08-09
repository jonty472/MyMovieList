using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace MyMovieApp
{
    internal class Program
    {

        /*
         * MVP -
         * GET request movie(s)
         * Deserialize movie GET request into a Movie object
         * Choose the correct movie to add to database
         * Have different users that hae their own movie list
         */

        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            /*
             * create a Task<t> that awaits a GET request for a movie
             * Pass the result of Task<t> into a method that deseralizes into an instances of an object
             * pass that object into the database
             */

            string movieRequestTask = await MovieList.GetMovieAysnc(client);
            List<Movie> movies = MovieList.DeserializingMovieAsync(movieRequestTask);
            MovieList.AddMovieToDb(movies);

        }
    }
}
