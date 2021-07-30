using Microsoft.EntityFrameworkCore;
using MoviesWeb.Context.ContextModels;
using MoviesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
           : base(options)
        {
            Database.EnsureCreated();

            //UserLists.Add(new UserListDb(1, 1));
            //MovieLists.Add(new MovieListDb(1, 15414));
            //SaveChanges();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserListDb>()
                .HasKey(e => new { e.User_id, e.List_id});

            modelBuilder.Entity<MovieListDb>()
                .HasKey(e => new { e.Id, e.Movie_id });
        }

        public DbSet<DirectorDb> Directors { get; set; }
        public DbSet<MovieDb> Movies { get; set; }
        public DbSet<MovieListDb> MovieLists { get; set; }
        public DbSet<UserListDb> UserLists { get; set; }
        public DbSet<PersonDb> People { get; set; }
        public DbSet<RatingDb> Ratings { get; set; }
        public DbSet<StarDb> Stars { get; set; }

    }
}

