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
                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {

                    cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = _username;
                    connection.Open();
                    int rowsEffected = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    connection.Close();
                    if (rowsEffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public string GetUsername()
        {
            return _username;
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