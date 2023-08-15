using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{
    public class MovieList
    {

        /// <summary>
        /// Class method will prompt a user via console for a movie title and release year
        /// </summary>
        /// <param name="client"></param>
        /// <returns>A jsonResponse string from The Movie Database (TMDB) API</returns>
        public static async Task<string> GetMovieAysnc(HttpClient client, string movietitle, string releaseyear)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query={movietitle}&year={releaseyear}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }

        
        /// <summary>
        /// deserialzes a jsonResponse into a movie object and adds the movies elements to the database
        /// </summary>
        /// <param name="jsonResponse"></param>
        public static void AddMovieToDb(string jsonResponse)
        {
     
            RootObject? movieRequest = JsonConvert.DeserializeObject<RootObject?>(jsonResponse);

            List<Movie> movies = new List<Movie>();

            foreach (var property in movieRequest.results)
            {
                movies.Add(new Movie() { id = property.id, title = property.title, release_date = property.release_date });
            }


            foreach (var movie in movies)
            {
                Console.WriteLine(movie.title);
                {
                    string cmdText =
                        "INSERT INTO Movies (MovieID, MovieTitle, ReleaseDate)" +
                        "VALUES (@ID, @Title, @ReleaseDate);";

                    using (SqlConnection connection = new SqlConnection(Program.connectionString))
                    {

                        using (SqlCommand command = new SqlCommand(cmdText, connection))
                        {

                            command.Parameters.Add("@ID", SqlDbType.Int).Value = movie.id;
                            command.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = movie.title;
                            command.Parameters.AddWithValue("@ReleaseDate", SqlDbType.Int).Value = DateTimeOffset.Parse(movie.release_date).ToUnixTimeSeconds();

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

