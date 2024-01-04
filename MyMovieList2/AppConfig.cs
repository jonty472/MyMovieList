using Microsoft.Extensions.Configuration;

public class AppConfig
{
    public string? ConnectionString { get; }
    public string? ApiKey { get; }

    public AppConfig(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("MyMovieList");
    }
}
