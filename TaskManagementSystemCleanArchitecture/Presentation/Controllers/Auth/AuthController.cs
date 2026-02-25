using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Auth;
using Application.DTOs.Auth;
namespace Presentation.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _mediator.Send(new RegisterCommand(request));
            return response.Succeeded? Ok(response) : BadRequest(response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _mediator.Send(new LoginCommand(request));
            return response.IsAuthenticated ? Ok(response) : Unauthorized(response.Message);
        }
    }
}
