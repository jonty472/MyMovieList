﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{
    public class Watchlist
    {
        List<Movie> movies;
        public Watchlist()
        {
            movies = new List<Movie>();
        }

        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }
        public List<Movie> GetMovies()
        {
            foreach(Movie movie in movies)
            {
                Console.WriteLine(movie.title);
            }
            return movies;
        }

        public async void LoadToDatabase(List<Movie> movies, string username)
        {
            string cmdText = "INSERT INTO Watchlist (UserID, MovieID) VALUES (" +
                "(SELECT UserID FROM Users WHERE Username = @Username), " +
                "@Id); ";

            foreach (Movie movie in movies)
            {

                try
                {
                    using (SqlConnection connection = new SqlConnection(cmdText))
                    {
                        using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                        {
                            cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar).Value = username;
                            cmd.Parameters.AddWithValue("@Id", SqlDbType.VarChar).Value = movie.id;
                            connection.Open();
                            Object result = await cmd.ExecuteNonQueryAsync();
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}