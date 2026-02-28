using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Contracts.Interfaces;
using Application.Contracts.Services;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tasks
{
    
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;
        

        public TaskService(ICurrentUserService userService, IUnitOfWork uow)
        {
            _currentUserService = userService;
            _uow = uow;
            
        }

        

        public async Task<string> CreateTaskAsync(CreateTaskDto dto)
        {
            var newTask = new TaskEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                Status = Status.Pending,
                DueDate = dto.DueDate,
                CreatedById = int.Parse(_currentUserService.UserId!)
            };
            await _uow.GenericRepository<TaskEntity>().AddAsync(newTask);
            await _uow.CompleteAsync();

            return $"Task `{newTask.Title}`  with {newTask.Id}  Created Successfully";
        }

       

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdAsync(id);
            _uow.GenericRepository<TaskEntity>().Delete(task);
            var result = await _uow.CompleteAsync();

            return result > 0;
        }

       

       

        public async Task<TaskResponseDto> GetTaskByIdAsync(int id)
        {
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdWithIncludesAsync(id,
                t => t.CreatedBy,
               t => t.AssignedUsers
               );

            var response = new TaskResponseDto
            {
                TaskId = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = task.CreatedAt,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                CreatedByUserName = task.CreatedBy.UserName,

                AssignedUsers = task.AssignedUsers.Select(tu => new AssignedUserDto
                {
                    UserId = tu.UserId,
                    UserName = tu.User.UserName,
                    UserEmail = tu.User.Email,
                    Feedback = tu.Feedback,
                    AssignedDate = tu.AssignedDate,
                    UserStatusInTask = tu.Status.ToString()

                }).ToList()
            };
            return response;


        }

        public async Task<IEnumerable<TaskResponseDto>> GetTasksAsync(string? searchTerm = null)
        {
            var tasks = await _uow.GenericRepository<TaskEntity>().GetAllWithIncludesAsync(t => t.CreatedBy,
               t => t.AssignedUsers
               );
            var response = tasks.Select(task => new TaskResponseDto
            {
                TaskId = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = task.CreatedAt,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                CreatedByUserName = task.CreatedBy.UserName,

                AssignedUsers = task.AssignedUsers.Select(tu => new AssignedUserDto
                {
                    UserId = tu.UserId,
                    UserName = tu.User.UserName,
                    UserEmail = tu.User.Email,
                    Feedback = tu.Feedback,
                    AssignedDate = tu.AssignedDate,
                    UserStatusInTask = tu.Status.ToString()

                }).ToList()
            });
            return response;
        }

       

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdAsync(id);
            if (task == null) return false;

            
            
            task.Title = dto.Title ?? task.Title;
            task.Description = dto.Description ?? task.Description;
            task.DueDate = dto.DueDate ?? task.DueDate;

           
            task.Status = dto.IsCompleted ? Status.Completed : Status.InProgress;

           
            _uow.GenericRepository<TaskEntity>().Update(task);
            var result = await _uow.CompleteAsync();

            return result > 0;


        }
    }
}
