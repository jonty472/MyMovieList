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
        /// returns void, but informs you if the there is a row
        /// </summary>
        /// <param name="args"></param>
        static void CheckRowExists(string[] args)
        {
            string connectionString = "your_connection_string";
            string conditionValue = "desired_value_to_check";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT 1 FROM @YourTable WHERE @ColumnName = @Value";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@YourTable", System.Data.SqlDbType.).Value = conditionValue;
                    command.Parameters.AddWithValue("@Value", conditionValue);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        Console.WriteLine("Row exists.");
                    }
                    else
                    {
                        Console.WriteLine("Row does not exist.");
                    }
                }
            }
        }
    }
}
