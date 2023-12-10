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
            User user = new User();
            bool showMenu = true;
            bool isLoggedIn = false;
            //Console.Write("Already have a watchlist? [Y/N]:");
            do
            {

                if (!isLoggedIn)
                {
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    user.Username = username;
                    if (await user.IsRegistered() == true)
                    {
                        isLoggedIn = true;
                    }
                    else
                    {
                        await user.RegisterUser(username);
                        isLoggedIn = true;
                    }
                }

                ShowMenu(user.Username);

                string choice = Console.ReadLine();

                if (await user.IsRegistered())
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Search Movies: ");
                            string movieTitle = Console.ReadLine();
                            if (!string.IsNullOrEmpty(movieTitle))
                            {
                                if (await movie.CheckDbForMovie(movieTitle))
                                {
                                    movie = await movie.GetMovieAsync(movieTitle);
                                }
                                else
                                {
                                    string movieRequest = await movie.GetMovieAsync(client, movieTitle);
                                    movie = movie.DeserializeMovieAsync(movieRequest);
                                    await movie.SaveToMovies();
                                }
                            }
                            break;
                        case "2":
                            myList.AddMovie(movie);
                            myList.SaveToWatchlist(myList.GetMovies(), user);
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            break;
                    }
                }
            } while (showMenu);

        }

        private static void ShowMenu(string username)
        {
            Console.Clear();
            Console.WriteLine($":{username}:");
            Console.WriteLine(
                    "1) Search for movie\n" +
                    "2) Add movie to watchlist\n" +
                    "3) Rate movie\n" +
                    "4) My watchlist\n" +
                    "5) Exit\n");

        }

        private static void ShowLoginMenu()
        {
            Console.WriteLine();
        }

        private static async void ShowRegisterMenu()
        {
            Console.WriteLine("======= Sign up ========");
            Console.Write("Username");
        }
    }
}
