using Application.Common;
using Application.Contracts.Services;
using Application.DTOs.UserTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Core
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly ITaskUserService _taskUserService;
       

        public UserTaskController(ITaskUserService taskUserService)
        {
            _taskUserService = taskUserService;
            
        }

        [HttpGet("UserTasks/Getmy-tasks")]
        public async Task<IActionResult> GetAllMyTasks()
        {
            var response = await _taskUserService.GetMyTasksAsync();

            return Ok(response);
        }

        [HttpPut("UserTasks/CompleteTask/{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var response = await _taskUserService.CompleteTaskAsync(id);

            if (response)
            {
                return Ok(response);
            }
            return BadRequest("Task doesn't exist");
        }

        [HttpPut("UserTasks/UncompleteTask/{id}")]
        public async Task<IActionResult> UncompleteTask(int id)
        {
            var response = await _taskUserService.UncompleteTaskAsync(id);

            if (response)
            {
                return Ok(response);
            }
            return BadRequest("Task doesn't exist");
        }

        [HttpPut("UserTasks/WriteFeedback/{id}")]
        public async Task<IActionResult> WriteFeedbackAsync(int id, FeedbackDto dto)
        {
            var response  = await _taskUserService.WriteFeedbackAsync(id, dto);

            if(response)
                return Ok("Feedback Written Successfully");

            return BadRequest("Task doesn't exist");
        }


    }
}
