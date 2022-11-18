using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApp.Server.Services;
using MovieApp.Shared.Models;
using System;
using System.Threading.Tasks;


namespace MovieApp.Server.Controllers
{
    [Authorize]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesDbService _dbService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _dbService.GetMovies();

            return Ok(await _dbService.GetMovies());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            var movie = await _dbService.GetMovie(id);
            if (movie == null) { return NotFound(); }
            return movie;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _dbService.DeleteMovie(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Movie movie)
        {
            await _dbService.AddMovie(movie);
            return Ok(movie.Id);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Movie movie)
        {
            await _dbService.UpdateMovie(movie);
            return Ok();
        }
    }
}
