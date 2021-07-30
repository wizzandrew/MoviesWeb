using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context.ContextModels
{
    [Keyless]
    public class DirectorDb
    {
        public int Movie_id { get; set; }
        public int Person_id { get; set; }
    }
}
