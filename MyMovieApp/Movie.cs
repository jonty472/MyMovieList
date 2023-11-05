using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{

    public class RootObject
    {
        public List<Movie> results { get; set; }
    }

    public class Movie
    {
        private double _userRating;
        public int id { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }

        public double vote_average { get; set; }

        public int vote_count { get; set; } 
    
        public double userRating 
        { 
            get
            {
                return _userRating;
            }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _userRating = value;
                }
                else
                {
                    _userRating = 0;
                }
            }
        }

    }
}
