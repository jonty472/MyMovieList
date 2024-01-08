using MyMovieList.Models;

namespace MyMovieList.Services;

public class BaseService
{
    public  readonly MyMovieListDbContext? _context;
    private int _value = 69;

    public int GetValue()
    {
        return _value;
    }

    public BaseService()
    {
        _context = new MyMovieListDbContext();
    }

    public MyMovieListDbContext GetMyMovieDbContext()
    {
        if (_context == null)
        {
            
        }
        return _context;
    }




}