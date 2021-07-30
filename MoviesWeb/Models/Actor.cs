using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Models
{
    public class Actor
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public string _birth { get; set; }

        public Actor(int id, string name, string birth)
        {
            _id = id;
            _name = name;
            _birth = birth;
        }
    }
}
