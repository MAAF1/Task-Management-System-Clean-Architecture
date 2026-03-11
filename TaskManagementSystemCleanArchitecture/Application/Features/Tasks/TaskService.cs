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
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            if (dto.AssignedUserIds != null && dto.AssignedUserIds.Any())
            {
                foreach (var userId in dto.AssignedUserIds)
                {
                   
                    var userExists = await _uow.GenericRepository<ApplicationUser>().GetByIdAsync(userId);
                    if (userExists == null) continue;

                    var assignedUser = new TaskUser
                    {
                        TaskId = newTask.Id,
                        UserId = userId,
                        AssignedDate = DateTime.UtcNow,
                        Status = Status.Pending
                    };
                    await _uow.GenericRepository<TaskUser>().AddAsync(assignedUser);
                }
            }

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
                q => q
        .Include(t => t.CreatedBy)
        .Include(t => t.AssignedUsers)
        .ThenInclude(tu => tu.User)
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
                CloseDate = task.ClosedDate,

                AssignedUsers = task.AssignedUsers.Select(tu => new AssignedUserDto
                {
                    UserId = tu.UserId,
                    UserName = tu.User.UserName,
                    UserEmail = tu.User.Email,
                    Feedback = tu.Feedback,
                    AssignedDate = tu.AssignedDate,
                    UserStatusInTask = tu.Status.ToString(),
                    UserClosedDate = tu.ClosedDate

                }).ToList()
            };
            return response;


        }

        public async Task<IEnumerable<TaskResponseDto>> GetTasksAsync(string? searchTerm = null)
        {
            var tasks = await _uow.GenericRepository<TaskEntity>().GetAllWithIncludesAsync(q => q
        .Include(t => t.CreatedBy)
        .Include(t => t.AssignedUsers)
        .ThenInclude(tu => tu.User));
        
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

           if(dto.IsCompleted && dto.IsInProgress)
                return false;
           if(dto.IsCompleted )
            {
                task.Status = Status.Completed;
                task.ClosedDate = DateTime.UtcNow;
            }
            else if(dto.IsInProgress)
            {
                task.Status = Status.InProgress;
                task.ClosedDate = null;
            }
           if(!dto.IsCompleted && !dto.IsInProgress) {
                task.Status = Status.Pending; 
                task.ClosedDate = null;
            }
            
           
            _uow.GenericRepository<TaskEntity>().Update(task);
            var result = await _uow.CompleteAsync();

            return result > 0;


        }
    }
}
