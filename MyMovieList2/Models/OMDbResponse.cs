using System;
using Newtonsoft.Json;

namespace MyMovieList.Models;

class OMDbResponse
{
    [JsonProperty("Search")]
    public List<Movie>? Movies { get; set; }
    [JsonProperty("totalResults")]
    public string? MovieCount { get; set; }
}