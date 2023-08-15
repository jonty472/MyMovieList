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
        public static int CheckRowExists(string conditionValue)
        {

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
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


        public static void EnterDataIntoMovieDB(int table, string value)
        {

        }
        
        
        public static int GetDatabaseRecordID(int movieIdOrUsername, string value)
        {

            string query = "";
            switch (movieIdOrUsername)
            {

                case 1:
                    query += "SELECT MovieID FROM Movies WHERE MovieTitle = @Value";
                    break;

                case 2:
                    query += "SELECT UserID FROM Users Where UserName = @Value";
                    break;
            }
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@Value", System.Data.SqlDbType.VarChar).Value = value;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int recordID = reader.GetInt32(0);
                            Console.WriteLine($"{recordID}");
                            return recordID;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
        }



    }
}
