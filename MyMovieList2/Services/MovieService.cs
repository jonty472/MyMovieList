using MyMovieList.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MyMovieList.Services;

public class MovieService : BaseService
{
    // use MyMovieListDbContext here for the UI logic (e.g. don't need to re-created the movie properties)
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
        return jsonResponse;

    }

    private void DeserializeMovie(string jsonResponse)
    {
        OMDbResponse? oMDbResponse = new OMDbResponse();
        oMDbResponse = JsonConvert.DeserializeObject<OMDbResponse>(jsonResponse);
        Console.WriteLine(oMDbResponse);
        // foreach (var movie in oMDbResponse.Movies)
        // {
            // Console.WriteLine($"{movie.Title} | {movie.Year}");
        //     Movie newMovie = new Movie()
        //     {
        //         Title = movie.Title,
        //         Year = movie.Year
        //     };

        //     _context.Add(newMovie);
        // }
        // _context.SaveChanges();
        // }

    }
    public async Task AddMovie(string title)
    {
    }
}