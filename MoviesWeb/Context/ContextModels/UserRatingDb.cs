using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context.ContextModels
{
    public class UserRatingDb
    {
        public int User_id { get; set; }
        public int Movie_id { get; set; }
        public float Rating { get; set; }

        public UserRatingDb() { }
        public UserRatingDb(int userId, int movieId, float rating)
        {
            User_id = userId;
            Movie_id = movieId;
            Rating = rating;
        }
    }
}
