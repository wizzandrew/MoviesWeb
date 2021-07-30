using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context.ContextModels
{
    [Keyless]
    public class RatingDb
    {
        public int Movie_id { get; set; }
        public float Rating{ get; set; }
        public int Votes { get; set; }
    }
}
