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
        public int id { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }

    }
}
