using Application.Contracts.Services;
using Application.DTOs;
using Application.Features.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
       // private readonly IMediator _mediator;
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        
        [HttpPost("AddTask")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var message = await _taskService.CreateTaskAsync(dto);
            return Ok(new { Message = message });
        }


        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            var response = await _taskService.GetTasksAsync();

            return Ok(response);
        }


        [HttpGet("GetTaskById/{id}")]
        public async Task<IActionResult> GetTaskByIdAsync(int id)
        {
            var response = await _taskService.GetTaskByIdAsync(id);
            return Ok(response);
        }


        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var response = await _taskService.DeleteTaskAsync(id);
            return Ok("Task deleted successfully");
        }


        [HttpPut("UpdateTask/{id}")]

        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {

            var response = await _taskService.UpdateTaskAsync(id, dto);
            if (response)
                return Ok("Task updated successfully");
            else
                return BadRequest("Can't update task");
        }


    }
}
