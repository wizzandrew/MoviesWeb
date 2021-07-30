using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesWeb.Context;
using MoviesWeb.Models;

namespace MoviesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        //[HttpGet]
        //public List<Movie> GetMovies()
        //{
        //    return _movieRepository.GetAllMovies();
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            if (id < 0) return BadRequest();
            return await _movieRepository.GetMovieById(id);
        }

        
        [HttpGet("title/{title}")]
        public ActionResult<List<Movie>> GetMovieByTitle(string title)
        {
            if (title == null) return BadRequest();
            return _movieRepository.GetMovieByTitle(title);
        }

        [HttpGet("top10")]
        public List<Movie> GetTopTenMovies()
        {
            return _movieRepository.GetTopTenMovies();
        }

        [HttpGet("average/{from},{to}")]
        public ActionResult<float> GetMoviesAvgRating(int from, int to)
        {
            if ((from < 1970 || to < 1970) ||
                (from > 2020 || to > 2020)) return BadRequest();
            return _movieRepository.GetMoviesAvgRating(from, to);

        }
    }
}