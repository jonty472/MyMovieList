using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using MyMovieApp.Helpers;

namespace MyMovieApp.Watchlist
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
                    correctMovie.vote_average = movie.vote_average;
                    correctMovie.vote_count = movie.vote_count;
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
            Console.WriteLine($"{movie.title} has been ADDED to your list");
            movieList.Add(movie);
        }

        private void AddWatchListToDatabase()
        {
            foreach (var movie in movieList)
            {
                if (IsMovieInDb())
                {
                    string cmd = "INSERT INTO Movies (Id, Title, ReleaseDate, AudienceRating, VoteCount) VALUES (@ID, @Title, @ReleaseDate, @AudienceRating, @VoteCount)";

                    using (SqlConnection connection = new SqlConnection(Program.connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(cmd, connection))
                        {
                            command.Parameters.Add("@Id", SqlDbType.BigInt).SqlValue = movie.id;
                            command.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = movie.title;
                            command.Parameters.AddWithValue("@ReleaseDate", SqlDbType.BigInt).Value = movie.release_date;
                            command.Parameters.AddWithValue("@AudienceRating", SqlDbType.Float).Value = movie.vote_average;
                            command.Parameters.AddWithValue("@VoteCount", SqlDbType.Float).Value = movie.vote_count;

                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }

        private bool IsMovieInDb()
        {
            return false;
        }

        public void RemoveMovie(Movie movie)
        {
            Console.WriteLine($"{movie.title} has been REMOVED from your movie list");
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
                    Console.WriteLine($"{movie.id}, {movie.title}, {movie.release_date}, {movie.userRating}");
                }
            }
        }
        
        public void SetMovieListRating()
        {
            double usersRating;
            foreach (var movie in movieList)
            {
                Console.Write($"{movie.title} RATING? ");

                usersRating = double.Parse(Console.ReadLine());

                if (usersRating == null)
                {
                    continue;
                }
                else if (usersRating != null || (usersRating >= 0 && usersRating <= 10))
                {
                    movie.userRating = usersRating;
                    /*
                    string cmd = "INSERT INTO Watchlist(UserRating) VALUES (@UserRating) WHERE Title = @MovieTitle";

                    using (SqlConnection connection = new SqlConnection(Program.connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(cmd))
                        {
                            command.Parameters.Add("@UserRating", SqlDbType.Float).Value = usersRating;
                            command.Parameters.AddWithValue("@MovieTitle", SqlDbType.VarChar).Value = movie.title;
                        }
                    }
                    */
                }

            }
        }

        public void GetMovieListRatings()
        {
            foreach (var movie in movieList)
            {
                Console.WriteLine($"{movie.id} | {movie.title} | {movie.userRating}");
            }
        }

        private void UpdateWatchlist(string updateField, Movie movie)
        {
            if (updateField == "title")
            {

            }
            else if (updateField == "releaseDate")
            {

            }
            else if (updateField == "")
            {

            }    
        }

        public void SortByUserRating()
        {
            movieList.Sort(new CompareByUsersRating());
        }
    }
}
