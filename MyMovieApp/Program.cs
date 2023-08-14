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

        /// <summary>
        /// HttpClient is intended to be instantiated once per application, rather than per-use.
        /// This is done to reduce the chances of a socket error
        /// </summary>
        public static readonly HttpClient client = new HttpClient();
        
        public static readonly string connectionString = "Server=DESKTOP-7O5A39Q\\SQLEXPRESS ;Integrated Security=true; Database=MovieDatabase;";
        static async Task Main(string[] args)
        {
            /*
             * create a Task<t> that awaits a GET request for a movie
             * Pass the result of Task<t> into a method that deseralizes into an instances of an object
             * pass that object into the database
             */

            Console.WriteLine("Movie? ");
            string? movietitle = Console.ReadLine();

            Console.WriteLine("Release Year? ");
            string? releaseyear = Console.ReadLine();

            string movieRequestTask = await MovieList.GetMovieAysnc(client, movietitle, releaseyear);
            Console.WriteLine(movieRequestTask);
            
            MovieList.AddMovieToDb(movieRequestTask);

        }
    }
}
