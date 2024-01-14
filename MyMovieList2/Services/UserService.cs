using MyMovieList.Models;

namespace MyMovieList.Services;

public class UserService : BaseService
{

    public bool IsLoggedIn { get; set; }
    public void AddUser(string username)
    {
        var user = new User()
        {
            Username = username,
            CreationDate = DateTime.Now
        };
        _context.Add(user);
        _context.SaveChanges();
    }

    public DateTime GetUserCreationDate(string username)
    {
       User? user = _context.Users.SingleOrDefault(user => user.Username == username);

       if (user != null)
       {
            return user.CreationDate;
       }
       else
       {
            throw new InvalidOperationException($"User with username '{username} not found'"); 
       }
    }

    public bool HasAccount(string username)
    {
        User? user = _context.Users.SingleOrDefault(user => user.Username == username);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
    }


}