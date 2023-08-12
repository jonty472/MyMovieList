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
        public User(string username)
        {
            _username = username;

            CreateUser(_username);
        }

        public string getUsername()
        {
            return _username;
        }

        public void CreateUser(string username)
        {

            string connectionString = "Server=DESKTOP-7O5A39Q\\SQLEXPRESS ;Integrated Security=true; Database=MovieDatabase;";

            string cmdText =
                "INSERT INTO Users (UserName)" +
                "VALUES (@UserName);";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public int AddMovieToWatchlist(string movietitle)
        {
            // if movie exist in database
                // insert movie into the UsersWatchlist table (use instance _username to insert into correcet watchlist).
            //else
                // AddMovieToDB();
                // call AddMovieToWatchlist() method again.

            if (string.IsNullOrEmpty(movietitle))
            {
                return 1;
            }

            string connectionString = "Server=DESKTOP-7O5A39Q\\SQLEXPRESS ;Integrated Security=true; Database=MovieDatabase;";

            string conditionValue = movietitle;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string commandText = "SELECT COUNT(*) FROM Movies.MovieTitle WHERE Movies.MovieTitle = @Value";

                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue(conditionValue, movietitle);

                    int rowCount = Convert.ToInt32(command.ExecuteScalar());

                    if (rowCount > 0)
                    {
                        Console.WriteLine("Row exists.");
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Row does not exist.");
                        MovieList.GetMovieAysnc(Program.client);
                        AddMovieToWatchlist(movietitle);
                        return 1;
                    }
                }
            }

        }

        public void DisplayUsersWatchlist()
        {
            // use _username as @UserName
            
        }

    }
}


