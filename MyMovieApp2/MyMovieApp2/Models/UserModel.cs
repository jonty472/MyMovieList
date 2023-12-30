namespace MyMovieApp2.Models;

public class User
{
    public int UserId {get; set;}
    public required string Username {get; set;}
    public ulong RegisteredDate {get; set;}
}