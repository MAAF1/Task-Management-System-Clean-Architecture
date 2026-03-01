using Application.Contracts.Services;
using Application.DTOs;
using Application.DTOs.UserTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Core
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentController : ControllerBase
    {
        private readonly ITaskAssignmentService _taskAssignmentService;
        
        public TaskAssignmentController(ITaskAssignmentService taskAssignmentService)
        {
            _taskAssignmentService = taskAssignmentService;
        }
        
        [HttpPut("TaskAssignment/AssignUsers/{id}")]
        public async Task<IActionResult> AssignUsers(int id, AssignTaskDto dto)
        {
            var response = await _taskAssignmentService.AssignUsersToTaskAsync(id, dto);

            if(response)
                return Ok("User Assigned Successfully");
            else return BadRequest("Couldn't Assign User");
        }


        [HttpPut("TaskAssignment/RemoveUsers/{id}")]
        public async Task<IActionResult> RemoveUsers(int id, RemoveUserDto dto)
        {
            var response = await _taskAssignmentService.RemoveUsersFromTaskAsync(id, dto);

            if (response)
                return Ok("User Removed Successfully");
            else return BadRequest("Couldn't Assign User");
        }

    }
}
