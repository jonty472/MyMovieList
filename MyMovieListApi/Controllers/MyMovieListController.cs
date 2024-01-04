// MoviesController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyMovieListApi.Models;

[ApiController]
[Route("[controller]")]
public class MyMovieListController : ControllerBase
{

    [HttpGet("GetAllMovies")]
    public IActionResult GetAllMovies()
    {
        return Ok("Testing Get request for all movies");
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById()
    {
        return Ok("Get movie by Id");
    }

    [HttpPost("AddMovie")]
    public IActionResult AddMovie()
    {
        return Ok("Post movie");
    }
}
