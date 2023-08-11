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
                    command.Parameters.Add("@UserName", SqlDbType.VarChar);


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
    }
}


