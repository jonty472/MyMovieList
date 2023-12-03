using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
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
            bool showMenu = true;
            //Console.Write("Already have a watchlist? [Y/N]:");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            User user = new User(username);
            do
            {

                ShowMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Search Movies: ");
                        string movieTitle = Console.ReadLine();
                        string movieRequest = await movie.GetMovieAysnc(client, movieTitle);
                        movie = movie.DeserializeMovieAsync(movieRequest);
                        break;
                    case "2":
                        myList.AddMovie(movie);
                        bool isRegistred = await user.IsRegistered(username);
                        if (!isRegistred)
                        {
                            break;
                        }
                        myList.LoadToDatabase(myList.GetMovies(), username);
                        break;
                }
            } while (showMenu);
        }

        private static void ShowMenu()
        {
            if (Login())
            {
                Console.Clear();
                Console.WriteLine(
                        "1) Search for movie\n" +
                        "2) Add movie to watchlist\n" +
                        "3) Rate movie\n" +
                        "4) Exit\n");

            }
        }

        private static bool Login()
        {
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            if (username != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
