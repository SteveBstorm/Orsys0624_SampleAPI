using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Infrastructure;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenManager _tokenManager;
        private readonly UserService _userService;

        public AuthController(TokenManager tokenManager, UserService userService)
        {
            _tokenManager = tokenManager;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginForm form)
        {
            User current = _userService.Login(form.Email, form.Password);
            if (current is null) return BadRequest("Erreur de connexion");

            return Ok(_tokenManager.GenerateToken(current));
        }
    }
}
