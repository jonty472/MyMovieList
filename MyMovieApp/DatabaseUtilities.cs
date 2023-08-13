using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{
    public class DatabaseUtilities
    {
        /// <summary>
        /// returns 0 if row exists else 1 if row does not exist.
        /// </summary>
        /// <param name="args"></param>
        public static int CheckRowExists(string conditionValue, string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Movies WHERE MovieTitle = @Value";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value", conditionValue);

                    int rowCount = Convert.ToInt32(command.ExecuteScalar());

                    if (rowCount > 0)
                    {
                        Console.WriteLine("Row exists.");
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Row does not exist.");
                        return 1;
                    }
                }
            }
        }
    }
}
