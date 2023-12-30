using Microsoft.Extensions.Configuration;
using MyMovieApp2.Models;
using System;
using System.IO;
using System.Text.Json;

class Program
{

    public static readonly HttpClient client = new HttpClient();
    static async Task Main()
    {
        // correct api get request https://www.omdbapi.com/?s=gladiator&y=2000&apikey={apiKey}
        // extra parameters should be done with &
        string jsonResponse = await client.GetStringAsync($"http://www.omdbapi.com/?s=gladiator&y=2000&apiKey=487670a8");
        JsonDocument jsonDocument = JsonDocument.Parse(jsonResponse);
        List<Movie> movies = new List<Movie>();

        using (var myMovieList = new MyMovieListDbContext()) 
        {
            foreach (JsonElement movieElement in jsonDocument.RootElement.EnumerateArray())
            {
                Movie movie = new Movie
                {
                    Title = movieElement.GetProperty("Title").GetString(),
                    ReleaseDate = (ulong)DateTimeOffset.Parse(movieElement.GetProperty("ReleaseDate").ToString()).ToUnixTimeSeconds()

                };
                myMovieList.Movies.Add(movie);
            }
        }

    }
}
