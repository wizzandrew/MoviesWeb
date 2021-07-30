using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Models
{
    public class Movie
    {
        public int _id { get; set; }
        public string _title { get; set; }
        public int _year { get; set; }
        public float _rating { get; set; }
        //public List<Actor> _actors { get; set; }
        //public List<Director> _directors { get; set; }

        public Movie (int id, string title, int year, float rating)
        {
            _id = id;
            _title = title;
            _year = year;
            _rating = rating;
            //_actors = actors;
            //_directors = directors;
        }

    }
}
