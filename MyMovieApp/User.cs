using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MyMovieApp
{
    internal class User
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public User(string username)
        {
            _username = username;

            CreateUser(_username);
        }

        public string GetUsername()
        {
            return _username;
        }

        public void CreateUser(string username)
        {

            string cmdText =
                "INSERT INTO Users (UserName)" +
                "VALUES (@UserName);";

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.Write(ex);
                    }
                }
            }
        }

        public static void AddMovieToWatchlist(string movietitle, string username)
        {
            // if movie exist in database
                // insert movie into the UsersWatchlist table (use instance _username to insert into correcet watchlist).
            //else
                // AddMovieToDB();
                // call AddMovieToWatchlist() method again.

            if (string.IsNullOrEmpty(movietitle))
            {
            }

            int movieID = DatabaseUtilities.GetDatabaseRecordID(1, movietitle);
            int userID = DatabaseUtilities.GetDatabaseRecordID(2, username);

            if (DatabaseUtilities.CheckRowExists(movietitle) == 0)
            {
                using (SqlConnection connection = new SqlConnection(Program.connectionString))
                {
                    connection.Open();

                    string cmdText = "INSERT INTO UsersWatchlist (MovieID, UserID) VALUES (@MovieID, @UserID)";

                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@MovieID", SqlDbType.Int).Value = movieID;
                        command.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = userID;
                    }
                }
                
            }

        }

        public static void GetUsersWatchlist(string username)
        {

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string query =
                    "SELECT MovieTitle, ReleaseDate FROM Movies" +
                    "JOIN UsersWatchlist on MovieID = Movies.MovieID" +
                    "JOIN Users on UserWatchlist.UserID = UserID" +
                    "WHERE UserName = @Value";

                using SqlCommand command = new SqlCommand(query, connection);
                {
                    command.Parameters.Add("@Value", SqlDbType.VarChar).Value = username;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string movieTitle = reader.GetString(1);
                            int releaseDate = reader.GetInt32(2);
                            Console.Write($"{movieTitle}{releaseDate}", movieTitle, releaseDate); ;
                        }
                    }
                }
            }
        }

    }
}



