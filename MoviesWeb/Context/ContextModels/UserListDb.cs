using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context.ContextModels
{
    public class UserListDb
    {
        public int User_id { get; set; }
        public int List_id { get; set; }

        public UserListDb() { }
        public UserListDb(int userId, int listId)
        {
            User_id = userId;
            List_id = listId;
        }
    }
}
