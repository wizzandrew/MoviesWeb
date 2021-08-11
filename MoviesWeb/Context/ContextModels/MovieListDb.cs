using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context.ContextModels
{
    public class MovieListDb
    {
        public int Id { get; set; }
        public int Movie_id { get; set; }

        public MovieListDb() { }
        public MovieListDb(int id, int movieId)
        {
            Id = id;   
            Movie_id = movieId;
        }

    }
}
