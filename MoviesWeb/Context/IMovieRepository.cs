using MoviesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWeb.Context
{
    public interface IMovieRepository
    {
        //movies
        Task<List<Movie>> GetMovies();
        List<Movie> GetTopTenMovies();
        float GetMoviesAvgRating(int from, int to);
        Task<Movie> GetMovieById(int id);
        List<Movie> GetMovieByTitle(string title);

        //actors
        Task<List<Actor>> GetActorsForMovie(int movieId);
        float GetActorsAvgRating(int id);

        //directors
        Task<List<Director>> GetDirectorsForMovie(int movieId);
        float GetDirectorsAvgRating(int id);

        //movie lists
        Task<MovieList> GetMovieList(int userId, int id);
        Task<List<MovieList>> GetMovieLists(int userId);
        Task<bool> MovieListExists(int userId, int listId);
        Task<MovieList> CreateMovieList(MovieList movieList);
        void UpdateMovieList(MovieList movieList);
        Task DeleteMovieList(int userId, int listId);
    }
}
