using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
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
        public IActionResult Create(Movie m)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            _movieService.Add(m);
            return Ok();
        }
    }
}
