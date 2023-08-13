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
            if (DatabaseUtilities.CheckRowExists(movietitle, connectionString) == 0)
            {
                return 0;
            }
            else
            {
                // Didn't program AddMovieToDB very well, so have to call all the other methods to get the parameter needed.
                Task<string> jsonResponse = MovieList.GetMovieAysnc(Program.client);
                string json = jsonResponse.ToString();

                List<Movie> movie = MovieList.DeserializingMovieAsync(json);


                MovieList.AddMovieToDb(movie);

                return 1;
            }
                
                
            

        }

        public void DisplayUsersWatchlist()
        {
            // use _username as @UserName
            
        }

    }
}


