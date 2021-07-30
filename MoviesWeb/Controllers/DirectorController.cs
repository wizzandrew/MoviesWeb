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
    public class DirectorController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public DirectorController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("rating/{id}")]
        public ActionResult<float> GetDirectorsAvgRating(int id)
        {
            if (id < 0) return BadRequest();
            return _movieRepository.GetDirectorsAvgRating(id);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<List<Director>>> GetDirectorsForMovie(int movieId)
        {
            if (movieId < 0) return BadRequest();
            return await _movieRepository.GetDirectorsForMovie(movieId);
        }
    }
}