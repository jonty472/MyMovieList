using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{
    public class User
    {
        private string _username;

        public string Username { set { _username = value; } get { return GetUsername(); } }

        public User()
        {
            _username = Username;
        }

        public async Task<bool> IsRegistered()
        {
            string cmdText = ("SELECT COUNT(*) FROM Users WHERE Username = @username");

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = _username;

                    object? result = await cmd.ExecuteScalarAsync();
                    int? value = Convert.ToInt32(result);
                    if (value == 1)
                    {
                        Console.WriteLine("User exists.");
                        Console.Write(value);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("User doesn't exist.");
                        return false;
                    }
                }
            }
        }

                        
        public string GetUsername()
        {
            return _username;
        }

        public int GetUserId()
        {
            string cmdText = "select UserId from Users where Username = @Username";

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@cmdText, connection))
                {
                    cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar).Value = _username;
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // column 0 is UserId
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
        }

        public async Task<int> RegisterUser(string username)
        {
            string cmdText = "INSERT INTO Users (Username) VALUES (@Username)";

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar).Value = username;
                        int result = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"New Account Created.");
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); return -1;
                }
            }
        }
    }
}