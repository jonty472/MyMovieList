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
        private string _username { get; set; }

        public User(string username)
        {
            _username = username;
        }

        public async Task<bool> IsRegistered(string username)
        {
            string cmdText = ("SELECT COUNT(*) FROM Users WHERE Username = @username");

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = username;
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
        }
    }
}