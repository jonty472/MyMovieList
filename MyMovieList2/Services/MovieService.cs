using MyMovieList.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MyMovieList.Services;

public class MovieService : BaseService
{
    // use MyMovieListDbContext here for the UI logic (e.g. don't need to re-created the movie property)
    private readonly HttpClient client = new HttpClient();
    private string? _apiKey;
    public int Test()
    {
        var test1 = new BaseService();
        return test1.GetValue();
    }
    // should probably also do DI with this as well e.g. should be handeled within AppConfig
    private void GetApiKey()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile($"{currentDirectory}/appsettings.json", true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        AppConfig appConfig = new AppConfig(config);
        _apiKey = appConfig.ApiKey;
    }
    
    public async Task<string> GetMovieAsync(string title)
    {
        GetApiKey();
        string jsonResponse = await client.GetStringAsync($"https://www.omdbapi.com/?s={title}&apiKey={_apiKey}");
        DeserializeMovie(jsonResponse);
        return jsonResponse;
    }
    public async Task<string> GetMovieAsync(string title, int releaseYear)
    {
        GetApiKey();
        string jsonResponse = await client.GetStringAsync($"https://www.omdbapi.com/?s={title}&y={releaseYear}&apiKey={_apiKey}");
        DeserializeMovie(jsonResponse);
        return jsonResponse;

    }

    private List<Movie> DeserializeMovie(string jsonResponse)
    {

        OMDbResponse? oMDbResponse = JsonConvert.DeserializeObject<OMDbResponse>(jsonResponse);
        List<Movie> movies = new List<Movie>();
        foreach (var property in oMDbResponse.Movies)
        {
            Movie movie = new Movie()
            {
                Title = property.Title,
                Year = property.Year,
                IMDbVotes = property.IMDbVotes,
                IMDbRating = property.IMDbRating
            };
            movies.Add(movie);
        }
        return movies;
    }
    public async Task AddMovie(string title)
    {
    }
}