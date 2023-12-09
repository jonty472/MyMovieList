using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{

    public class RootObject
    {
        public List<Movie>? results { get; set; }
    }

    public class Movie
    {
        private double _userRating;
        public int id { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }

        public double vote_average { get; set; }

        public int vote_count { get; set; } 
    
        public double userRating 
        { 
            get
            {
                return _userRating;
            }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _userRating = value;
                }
                else
                {
                    _userRating = 0;
                }
            }
        }
        
        public Movie()
        {
            this.id = 0;
            this.title = "";
            this.release_date = "";
            this.vote_average = 0;
            this.vote_count = 0;
        }

        public Movie(double userRating, int id, string release_date, string title, double vote_average, int vote_count)
        {
            this.id = id;
            this.title = title;
            this.release_date = release_date;
            this.vote_average = vote_average;
            this.vote_count = vote_count;
        }

        public async Task<string> GetMovieAysnc(HttpClient client, string title, int releaseDate)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query={title}&year={releaseDate}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
        public async Task<string> GetMovieAysnc(HttpClient client, string title)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query={title}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
        public Movie DeserializeMovieAsync(string jsonResponse)
        {
            RootObject? movies = JsonConvert.DeserializeObject<RootObject>(jsonResponse);

            List<Movie> movieList = movies.results;

            Movie myMovie = new Movie();

            foreach (var movie in movieList)
            {
                bool correctMovie = CorrectMovie(movie.title, movie.release_date);
                if (correctMovie)
                {
                    myMovie.title = movie.title;

                    myMovie.id = movie.id;
                    myMovie.title = movie.title;
                    myMovie.release_date = movie.release_date;
                    myMovie.vote_average = movie.vote_average;
                    myMovie.vote_count = movie.vote_count;
                    break;
                }
                else
                {
                    continue;
                }
            }

            return myMovie;
        }

        private bool CorrectMovie(string movieTitle, string releaseDate)
        {
            Console.Write($"{movieTitle} ({releaseDate}) Y/N: ");
            string response = Console.ReadLine();
            if (response == null)
            {
                return false;
            }
            else if (response == "Y" || response == "y")
            {
                return true;
            }
            else if (response == "N" || response == "n")
            {
                return false;
            }
            else
            {
                return false;
            }
        }

    }
}
