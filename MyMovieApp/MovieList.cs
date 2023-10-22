using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace MyMovieApp
{

    public class MovieList
    {
        private List<Movie> movieList;
        public MovieList()
        {
            movieList = new List<Movie>();
            //movieList.Add(new Movie() { id = 22, title = "Test", release_date = "2023" });


        }

        // get movie
        // add movie
        // remove movie
        // update movie
        // get movie list

        public async Task<Movie> GetMovieAysnc(HttpClient client, string title, int releaseYear)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query={title}&year={releaseYear}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Movie movie = DeserializingMovieAsync(jsonResponse);
            return movie;
        }

        private Movie DeserializingMovieAsync(string jsonResponse)
        {
            RootObject? movies = JsonConvert.DeserializeObject<RootObject?>(jsonResponse);
            
            Movie correctMovie = new Movie();

            foreach (var movie in movies.results)
            {
                if (IsCorrectMovie(movie))
                {
                    correctMovie.id = movie.id;
                    correctMovie.title = movie.title;
                    correctMovie.release_date = movie.release_date;
                    break;
                }
                else
                {
                    continue;
                }
            }
        
            return correctMovie;
            
        }

        private bool IsCorrectMovie(Movie movie)
        {
            bool isCorrectMovie = false;
            
            Console.Write($"Your movie: {movie.title}? Y/N: ");
            string? chooseYesNo = Console.ReadLine();
            if (chooseYesNo.ToLower() == "y")
            {
                isCorrectMovie = true;
            }
            else
            {
                return false;
            }
            return isCorrectMovie;
        }

        public void AddMovie(Movie movie)
        {
            Console.WriteLine($"{movie.title} has been added to your list");
            movieList.Add(movie);
        }

        public void RemoveMovie(Movie movie)
        {
            movieList.Remove(movie);
        }

        public void DisplayMovieList()
        {
            foreach (var movie in movieList)
            {
                if (movieList.Count < 1)
                {
                    Console.WriteLine("Your movie list is empty");
                }
                else
                {
                    Console.WriteLine($"{movie.id}, {movie.title}, {movie.release_date}");
                }
            }
        }

    }
}
