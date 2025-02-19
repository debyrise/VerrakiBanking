using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerrakiBanking.Business.Services.Interface;
using VerrakiBanking.Data.DTOs;

namespace VerrakiBankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthservice _authService;

        public AuthController(IAuthservice authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginUserAsync(model);

            if (!result.Succeeded)
                return Unauthorized(result.Message);  

            return Ok(new { Message = result.Message, Token = result.Token });
        }

    }
}
