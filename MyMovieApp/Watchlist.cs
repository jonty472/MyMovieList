using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public List<Movie> GetMovies()
        {
            foreach(Movie movie in movies)
            {
                Console.WriteLine(movie.title);
            }
            return movies;
        }

        public async void SaveToWatchlist(List<Movie> movies, User user)
        {
            string cmdText = "INSERT INTO Watchlist (UserID, MovieID) " +
                "VALUES (@UserId, @MovieId);";

            int userId = user.GetUserId();

            foreach (Movie movie in movies)
            {

                try
                {
                    using (SqlConnection connection = new SqlConnection(Program.connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                        {
                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                            cmd.Parameters.AddWithValue("@MovieId", SqlDbType.Int).Value = movie.id;
                            int rowsEffected = await cmd.ExecuteNonQueryAsync();

                            if (rowsEffected > 0)
                            {
                                Console.WriteLine("Data inserted into Watchlist table");
                            }
                            else
                            {
                                Console.WriteLine("Data inserted into Watchlist table");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { Console.WriteLine("movie(s) have been added to your watchlist");  }
            }

        }

        public async Task<int> WatchlistCount(int userId)
        {
            string cmdText = "SELECT COUNT(*) " +
                             "FROM Watchlist " +
                             "WHERE UserId = @UserId;";

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        public async Task ViewWatchlist(int userId)
        {
            string cmdText = "SELECT Title " +
                            "FROM Watchlist INNER JOIN Movies ON Watchlist.MovieId = Movies.MovieId " +
                            "WHERE Watchlist.UserId = 1;";


            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                Console.WriteLine($"{reader["Title"].ToString()}");
                            }
                        }
                    }
                }
            }
        }
    }
}
