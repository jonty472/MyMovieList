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

        public void AddMovieToWatchlist(string movietitle)
        {
            // if movie exist in database
                // insert movie into the UsersWatchlist table (use instance _username to insert into correcet watchlist).
            //else
                // AddMovieToDB();
                // call AddMovieToWatchlist() method again.

            if (string.IsNullOrEmpty(movietitle))
            {
            }

            if (DatabaseUtilities.CheckRowExists(movietitle) == 0)
            {
                //string cmdText = "INSERT INTO UsersWatchlist (UserID, MovieID) VALUES (@UserID, @MovieID)";
                string cmdText = "SELECT * FROM Movies WHERE MovieTitle = @movietitle";


                using (SqlConnection connection = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.Parameters.Add("@movietitle", SqlDbType.VarChar).Value = movietitle;
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                Console.WriteLine("Movie doesn't exist in movie database");  
            }

        }

        public void DisplayUsersWatchlist()
        {
            // use _username as @UserName
            
        }

    }
}



