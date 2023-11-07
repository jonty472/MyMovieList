using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp.Helpers
{
    public class CompareByUsersRating : IComparer<Movie>
    {
        public int Compare(Movie movie_x, Movie movie_y)
        {
            if (movie_x.userRating >  movie_y.userRating)
            {
                return 1;
            }
            else if (movie_x.userRating < movie_y.userRating)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
