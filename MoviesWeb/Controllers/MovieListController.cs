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
    public class MovieListController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieListController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{userId},{listId}")]
        public async Task<ActionResult<MovieList>> GetMovieList(int userId, int listId)
        {
            if (userId < 0 || listId < 0) return BadRequest();

            if(await _movieRepository.MovieListExists(userId, listId))
            {
                return await _movieRepository.GetMovieList(listId);
            }
            else
            {
                return NotFound();
            }  
        }

        [HttpPost]
        public async Task<ActionResult<MovieList>> CreateMovieList([FromBody] MovieList movieList)
        {
            var newMovieList = await _movieRepository.CreateMovieList(movieList);
            if (newMovieList != null)
            {
                return CreatedAtAction(nameof(GetMovieList), new { id = newMovieList._id }, newMovieList);
            }
            else return BadRequest("there cant be one more list with the same ID in the userlists table in database");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMovieList(int userId, int listId, [FromBody] MovieList movieList)
        {
            if (userId < 0 || listId < 0) return BadRequest();
            if (listId != movieList._id) return BadRequest();
            if (movieList._movieIds == null) return BadRequest();

            if (await _movieRepository.MovieListExists(userId, listId))
            {
                _movieRepository.UpdateMovieList(movieList);

                return NoContent();
            } 
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{userId},{listId}")]
        public async Task<ActionResult> DeleteMovieList(int userId, int listId)
        {
            var listToDelete = await _movieRepository.GetMovieList(listId);
            if (listToDelete == null) return NotFound();

            await _movieRepository.DeleteMovieList(userId, listId);
            return NoContent();
        }
    }
}