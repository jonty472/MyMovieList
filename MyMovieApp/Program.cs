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
        public class RootObject
        {
            public List<Movie> results { get; set; }
        }
        public class Movie
        {
            public int id { get; set; }
            public string release_date { get; set; }
            public string title { get; set; }
            
        }

        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            /*
             * create a Task<t> that awaits a GET request for a movie
             * Pass the result of Task<t> into a method that deseralizes into an instances of an object
             * pass that object into the database
             */

            string movieRequestTask = await GetMovieAysnc(client);
            List<Movie> movies = DeserializingMovieAsync(movieRequestTask);
            AddMovieToDb(movies);

        }

        public static async Task<string> GetMovieAysnc(HttpClient client)
        {
            Console.WriteLine("What movie are you looking for?");
            string movieTitle = Console.ReadLine();
            Console.WriteLine($"What year was it released?");
            string releaseYear = Console.ReadLine();

            using HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query={movieTitle}&year={releaseYear}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }

        public static List<Movie> DeserializingMovieAsync(string jsonResponse)
        {
            RootObject? movies = JsonConvert.DeserializeObject<RootObject?>(jsonResponse);

            List<Movie> movie = new List<Movie>();

            foreach (var property in movies.results)
            {
                movie.Add(new Movie() { id = property.id, title = property.title, release_date = property.release_date });
            }

            foreach (var i in movie)
            {
                Console.WriteLine(i.title + i.release_date + i.title);
            }

            return movie;
        }

        public static void AddMovieToDb(List<Movie> movies)
        {


            foreach (var movie in movies)
            {
                Console.WriteLine(movie.title);
                Console.WriteLine("Is this the movie you are trying to add? (Y/N): ");
                var response = Console.ReadLine();
                if (movies.Count == 0)
                {
                    Console.WriteLine("couldn't find that movie");
                    break;
                }
                if (response == "N")
                {
                    continue;
                }
                if (response == "Y")
                {
                    string connectionString = "Server=DESKTOP-7O5A39Q\\SQLEXPRESS ;Integrated Security=true; Database=MovieDatabase;";

                    string cmdText =
                        "INSERT INTO Movies (ID, MovieTitle, ReleaseYear)" +
                        "VALUES (@ID, @Title, @ReleaseYear);";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        using (SqlCommand command = new SqlCommand(cmdText, connection))
                        {

                            command.Parameters.Add("@ID", SqlDbType.Int).Value = movie.id;
                            command.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = movie.title;
                            command.Parameters.AddWithValue("@ReleaseYear", SqlDbType.Int).Value = DateTimeOffset.Parse(movie.release_date).ToUnixTimeSeconds();

                            try
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                                break;

                            }
                            catch (SqlException ex)
                            {
                                Console.Write(ex);
                            }

                        }

                    }
                }
            }

        }
    }

}

