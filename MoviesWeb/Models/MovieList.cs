using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Models
{
    public class MovieList
    {
        public int _id { get; set; }
        public int _userId { get; set; }
        public int[] _movieIds { get; set; }
        public List<Movie> _movies { get; set; }

        public MovieList() { }
        public MovieList(int id, int userId, int[] movieIds)
        {
            _id = id;
            _userId = userId;
            _movieIds = movieIds;
        }
        public MovieList(int id,List<Movie> movies)
        {
            _id = id;
            _movies = movies;
        }
    }
}
