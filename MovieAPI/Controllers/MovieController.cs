using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Hubs;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        private readonly MovieHub _movieHub;

        public MovieController(MovieService movieService, MovieHub movieHub)
        {
            _movieService = movieService;
            _movieHub = movieHub;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_movieService.Liste);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            return Ok(_movieService.GetById(id));
        }

        [Authorize("adminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(Movie m)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            _movieService.Add(m);
            await _movieHub.NewMovie();
            return Ok();
        }
    }
}
