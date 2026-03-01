using Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Core
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Users/GetAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync() 
        {
            
            var response = await _userService.GetAllUsersAsync();

            return Ok(response);
        
        }

        [HttpGet("Users/GetUser/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {

            var response = await _userService.GetByIdAsync(id);

            return Ok(response);

        }
    }
}
