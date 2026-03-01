using Application.Common;
using Application.Contracts.Interfaces;
using Application.Contracts.Services;
using Application.DTOs;
using Application.DTOs.UserTasks;
using Domain.Entities;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tasks
{
    public class TaskUserService : ITaskUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;


        public TaskUserService(ICurrentUserService userService, IUnitOfWork uow)
        {
            _currentUserService = userService;
            _uow = uow;

        }

        public async Task<bool> CompleteTaskAsync(int id)
        {
            int currentUserId = int.Parse(_currentUserService.UserId!);
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdAsync(id);
            if(task == null) { return false; }

            var tasks = await _uow.GenericRepository<TaskUser>().GetAllAsync();
            var selectedTask = tasks
             .FirstOrDefault(au => au.TaskId == id && au.UserId == currentUserId);
           if(selectedTask == null) { return false; }
            selectedTask.Status = Status.Completed;

            _uow.GenericRepository<TaskUser>().Update(selectedTask);

            var response = await _uow.CompleteAsync();

            return response > 0;

            
        }

        public async Task<UserTaskDetailsDto> GetMyTasksAsync(string? searchTerm = null)
        {
            int currentUserId = int.Parse(_currentUserService.UserId!);
            var user = await _uow.GenericRepository<ApplicationUser>().GetByIdAsync(currentUserId);
            if (user == null) return null;
            var userAssignments = await _uow.GenericRepository<TaskUser>().GetAllWithIncludesAsync(
            tu => tu.Task,
            tu => tu.Task.CreatedBy 
            );

            var myAssignments = userAssignments.Where(tu => tu.UserId == currentUserId);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                myAssignments = myAssignments.Where(tu =>
                    tu.Task.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            return new UserTaskDetailsDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserEmail = user.Email,
                AssignedTasks = myAssignments.Select(tu => new TaskDetailsDto
                {
                    TaskId = tu.TaskId,
                    Title = tu.Task.Title,
                    Description = tu.Task.Description,
                    CreatedBy = tu.Task.CreatedBy.UserName,
                    CreatedDate = tu.Task.CreatedAt,
                    DueDate = tu.Task.DueDate,
                    ClosedDate = tu.Task.ClosedDate, 
                    Status = tu.Task.Status.ToString(),
                    
                    TaskUserAssignedDate = tu.AssignedDate,
                    UserClosedDate = tu.ClosedDate, 
                    UserTaskStatus = tu.Status.ToString() 
                }).ToList()




            };
        }

        public async Task<bool> UncompleteTaskAsync(int id)
        {
            int currentUserId = int.Parse(_currentUserService.UserId!);
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdAsync(id);
            if (task == null) { return false; }

            var tasks = await _uow.GenericRepository<TaskUser>().GetAllAsync();
            var selectedTask = tasks
             .FirstOrDefault(au => au.TaskId == id && au.UserId == currentUserId);
            if (selectedTask == null) { return false; }
            selectedTask.Status = Status.InProgress;

            _uow.GenericRepository<TaskUser>().Update(selectedTask);

            var response = await _uow.CompleteAsync();

            return response > 0;

        }

        public async Task<bool> WriteFeedbackAsync(int id, FeedbackDto dto)
        {
            int currentUserId = int.Parse(_currentUserService.UserId!);
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdAsync(id);
            if (task == null) { return false; }

            var tasks = await _uow.GenericRepository<TaskUser>().GetAllAsync();
            var selectedTask = tasks
             .FirstOrDefault(au => au.TaskId == id && au.UserId == currentUserId);
            if (selectedTask == null) { return false; }
            selectedTask.Feedback = dto.Feedback;

            _uow.GenericRepository<TaskUser>().Update(selectedTask);

            var response = await _uow.CompleteAsync();

            return response > 0;

        }
    }
}
