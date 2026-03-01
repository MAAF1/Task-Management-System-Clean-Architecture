using Application.Contracts.Interfaces;
using Application.Contracts.Services;
using Application.DTOs;
using Domain.Entities;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tasks
{
    public class TaskAssignmentService : ITaskAssignmentService
    {
        private readonly  IUnitOfWork _uow;
        public TaskAssignmentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> AssignUsersToTaskAsync(int taskId, AssignTaskDto dto)
        {
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdAsync(taskId);
            if (task == null)
            {
                return false;
            }

            var exisitngAssignments = await _uow.GenericRepository<TaskUser>().GetAllAsync();

            var currentUserIds = exisitngAssignments.Where(au => au.TaskId == taskId).Select(au => au.UserId).ToList();
            
            foreach(var userId in dto.UserIds)
            {
                var userExists = await _uow.GenericRepository<ApplicationUser>().GetByIdAsync(userId);
                if (userExists == null) 
                    continue;
                if (currentUserIds.Contains(userId)) continue;

                var assignedUser = new TaskUser
                {
                    TaskId = taskId,
                    UserId = userId,
                    AssignedDate = DateTime.UtcNow,
                    Status = Status.Pending
                };

                await _uow.GenericRepository<TaskUser>().AddAsync(assignedUser);


            }

            var response = await  _uow.CompleteAsync();

            return response > 0;
        }

        public async Task<bool> RemoveUsersFromTaskAsync(int taskId, RemoveUserDto dto)
        {
            var task = await _uow.GenericRepository<TaskEntity>().GetByIdAsync(taskId);
            if (task == null)
            {
                return false;
            }

            var allAssignments = await _uow.GenericRepository<TaskUser>().GetAllAsync();

            var assignmentsToRemove = allAssignments
            .Where(au => au.TaskId == taskId && dto.UserIds.Contains(au.UserId))
            .ToList();


            if (!assignmentsToRemove.Any()) return false;

            
            foreach (var assignment in assignmentsToRemove)
            {
                _uow.GenericRepository<TaskUser>().Delete(assignment);
            }




            var response = await _uow.CompleteAsync();

            return response > 0;
        }
    }
}
