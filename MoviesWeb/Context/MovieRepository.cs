using Microsoft.EntityFrameworkCore;
using MoviesWeb.Context.ContextModels;
using MoviesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext movieContext)
        {
            _context = movieContext;
        }
        #region Movie
        public async Task<Movie> GetMovieById(int id)
        {
            var movieDb = await _context.Movies.FindAsync(id);
            if (movieDb == null) return null;

            var movieRating = getMovieRating(movieDb.Id);

            return new Movie(movieDb.Id, movieDb.Title, movieDb.Year, movieRating);
        }

        public List<Movie> GetMovieByTitle(string title)
        {
            List<Movie> movies = new List<Movie>();

            var movieDbs = _context.Movies.Where(x => x.Title.Equals(title)).ToList();
            if (movieDbs == null) return null;

            foreach (MovieDb movieDb in movieDbs)
            {
                var movieRating = getMovieRating(movieDb.Id);
                movies.Add(new Movie(movieDb.Id, movieDb.Title, movieDb.Year, movieRating));
            }
            return movies;
        }

        public float getMovieRating(int movieId)
        {
            float movieRating = 0f;
            var ratingDb = (RatingDb)_context.Ratings.Where(x => x.Movie_id == movieId).FirstOrDefault();
            if (ratingDb != null) movieRating = ratingDb.Rating;
            return movieRating;
        }
        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>();
            var moviesDb = _context.Movies.ToList();
            foreach (MovieDb movieDb in moviesDb)
            {
                var movieRating = getMovieRating(movieDb.Id);
                movies.Add(new Movie(movieDb.Id, movieDb.Title, movieDb.Year, movieRating));
            }

            //return movies
            return movies;
        }

        public List<Movie> GetTopTenMovies()
        {
            List<Movie> movies = new List<Movie>();
            var ratings = _context.Ratings.OrderByDescending(x => x.Rating).ToList().GetRange(0, 10);
            foreach(RatingDb rating in ratings)
            {
                var movie = GetMovieById(rating.Movie_id);
                movies.Add(movie.Result);
            }
            return movies;
        }

        public float GetMoviesAvgRating(int from, int to)
        {
            float rating = 0f;
            List<MovieDb> movies;
            if (from == to) movies = _context.Movies.Where(x => x.Year == from).ToList();
            else movies = _context.Movies.Where(x => x.Year >= from && x.Year <= to).ToList();

            //reading from db - bug
            Console.WriteLine(movies.Count);

            if (movies == null) return 0f;
            if(movies.Count > 0)
            {
                for(int i=0; i<movies.Count; i++)
                {
                    var tempRating = (RatingDb)_context.Ratings.Where(x => x.Movie_id == movies[i].Id).FirstOrDefault();
                    if (tempRating != null) rating += tempRating.Rating;
                }
                return (float)rating / movies.Count;
            }
            return rating;
        }

        
        #endregion


        #region Actor
        public async Task<List<Actor>> GetActorsForMovie(int movieId)
        {
            List<Actor> actors = new List<Actor>();
            var actorsDb = (List<StarDb>)_context.Stars.Where(x => x.Movie_id == movieId).ToList();

            if (actorsDb == null) return null;
            if (actorsDb != null && actorsDb.Count == 0) return null;

            for (int i = 0; i < actorsDb.Count; i++)
            {
                var person = await _context.People.FindAsync(actorsDb[i].Person_id);
                if (person != null)
                {
                    actors.Add(new Actor(person.Id, person.Name, person.Birth));
                }
            }
            return actors;
        }

        public float GetActorsAvgRating(int id)
        {
            float rating = 0f;
            var stars = _context.Stars.Where(x => x.Person_id == id).ToList();

            if (stars == null) return 0f;
            if (stars.Count > 0)
            {
                foreach (StarDb star in stars)
                {
                    var tmpRating = (RatingDb)_context.Ratings.Where(x => x.Movie_id == star.Movie_id).FirstOrDefault();
                    if (tmpRating != null) rating += tmpRating.Rating;
                }

                return (float)rating / stars.Count;
            }
            return rating;

        }
        #endregion


        #region Director
        public async Task<List<Director>> GetDirectorsForMovie(int movieId)
        {
            List<Director> directors = new List<Director>();
            var directorsDb = (List<DirectorDb>)_context.Directors.Where(x => x.Movie_id == movieId).ToList();

            if (directorsDb == null) return null;
            if (directorsDb != null && directorsDb.Count == 0) return null;
            for (int i = 0; i < directorsDb.Count; i++)
            {
                var person = await _context.People.FindAsync(directorsDb[i].Person_id);
                if (person != null)
                {
                    directors.Add(new Director(person.Id, person.Name, person.Birth));
                }
            }
            return directors;
        }
        public float GetDirectorsAvgRating(int id)
        {
            float rating = 0f;
            var directors = _context.Directors.Where(x => x.Person_id == id).ToList();

            if (directors == null) return 0f;
            if (directors.Count > 0)
            {
                foreach (DirectorDb director in directors)
                {
                    var tmpRating = (RatingDb)_context.Ratings.Where(x => x.Movie_id == director.Movie_id).FirstOrDefault();
                    if (tmpRating != null) rating += tmpRating.Rating;
                }

                return (float)rating / directors.Count;
            }
            return rating;
        }
        #endregion


        #region MovieList
        public async Task<MovieList> GetMovieList(int id)
        {
            List<Movie> movies = new List<Movie>();
            var movieListDbs =  await _context.MovieLists.Where(x => x.Id == id).ToListAsync();
            if (movieListDbs == null) return null;
            if (movieListDbs != null && movieListDbs.Count == 0) return null;

            for(int i=0; i< movieListDbs.Count; i++) {
                var movie = await GetMovieById(movieListDbs[i].Movie_id);
                if (movie != null) movies.Add(movie);
            }
            return new MovieList(id, movies);

        }

        public async Task<bool> MovieListExists(int userId, int listId)
        {
            var userListDb = await _context.UserLists.FindAsync(userId, listId);
            if (userListDb == null) return false;
            else return true;
        }

        public async Task<MovieList> CreateMovieList(MovieList movieList)
        {
            //there cant be one more list with the same ID in the userlists table in database
            if (_context.UserLists.Where(x => x.List_id == movieList._id).FirstOrDefault() != null) return null;

            //there cant be one more record with same list Id and same movie Id in the movielists table in database
            //if(_context.MovieLists.Where(x => x.Id == movieList._id).FirstOrDefault() != null)
            //{
            //    for(int i=0; i < movieList._movieIds.Length; i++)
            //    {
            //        if(_context.MovieLists.Where(x => x.Movie_id == movieList._movieIds[i]).FirstOrDefault() != null)
            //        {
            //            return null;
            //        }
            //    }
            //}

            var userlistDb = new UserListDb(movieList._userId, movieList._id);
            _context.UserLists.Add(userlistDb); 
            _context.SaveChanges();
            _context.Entry<UserListDb>(userlistDb).State = EntityState.Detached; 
            

            for (int i=0; i<movieList._movieIds.Length; i++)
            {
                var movieListDb = new MovieListDb(movieList._id, movieList._movieIds[i]); 
                _context.MovieLists.Add(movieListDb);
                _context.SaveChanges();
                _context.Entry<MovieListDb>(movieListDb).State = EntityState.Detached;
            }
            return movieList;
        }

        public async void UpdateMovieList(MovieList movieList)
        {
            var movieListsToDelete = _context.MovieLists.Where(x => x.Id == movieList._id);
            foreach(var movieListDb in movieListsToDelete)
            {             
                _context.MovieLists.Remove(movieListDb);
                _context.SaveChanges();
            }

            for (int i = 0; i < movieList._movieIds.Length; i++)
            {
                var movieListToWrite = new MovieListDb(movieList._id, movieList._movieIds[i]);
                _context.MovieLists.Add(movieListToWrite);
                _context.SaveChanges();
                _context.Entry<MovieListDb>(movieListToWrite).State = EntityState.Detached;
            }

        }

        public async Task DeleteMovieList(int userId, int listId)
        {
            //delete userlist first in db
            var userListToDelete = await _context.UserLists.FindAsync(userId, listId);
            if (userListToDelete == null) return;
            _context.UserLists.Remove(userListToDelete);
            await _context.SaveChangesAsync();

            //delete movielist(s) in db
            var moviesToDelete = await _context.MovieLists.Where(x => x.Id == listId).ToListAsync();
            if (moviesToDelete == null) return;
            foreach(MovieListDb movieListDb in moviesToDelete)
            {
                _context.MovieLists.Remove(movieListDb);
                await _context.SaveChangesAsync();
            }
        }
        #endregion



    }
}
