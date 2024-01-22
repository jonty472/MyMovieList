
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyMovieListBlazorApp.Services
{
    public class MovieService
    {

        private HttpClient _httpClient;
        public MovieService(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }

        public Movie GetMovieAsync()
        {
            string jsonResponse = "{\"Search\":[{\"Title\":\"Oldboy\",\"Year\":\"2003\",\"imdbID\":\"tt0364569\",\"Type\":\"movie\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BMTI3NTQyMzU5M15BMl5BanBnXkFtZTcwMTM2MjgyMQ@@._V1_SX300.jpg\"}],\"totalResults\":\"1\",\"Response\":\"True\"}";
            OMDbResponse? movies = JsonConvert.DeserializeObject<OMDbResponse>(jsonResponse);
            Movie movie = new Movie();
            if (!string.IsNullOrEmpty(jsonResponse))
            {
                foreach (var property in movies.Response)
                {
                    movie.Title = property.Title;
                    movie.Year = property.Year; 
                }
            }
            return movie;
        }
    }
}