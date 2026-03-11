using Application.Contracts.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public Task AddAsync(TaskEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TaskEntity entity)
        {
            
        }

        

        public async Task<List<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks.Include(tu => tu.AssignedUsers).Include(c => c.CreatedBy).ToListAsync();

            var result = tasks.Select(task => new TaskResponseDto
            {
                TaskId = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = task.CreatedAt,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                CloseDate = task.ClosedDate,
                CreatedByUserName = task.CreatedBy.UserName,
                AssignedUsers = task.AssignedUsers.Select(tu => new AssignedUserDto
                {
                    UserId = tu.User.Id,
                    UserName = tu.User.UserName,
                    UserEmail = tu.User.Email,
                    UserClosedDate = tu.ClosedDate,
                    UserStatusInTask = tu.Status.ToString(),
                    AssignedDate = tu.AssignedDate,
                    Feedback = tu.Feedback


                }).ToList()
            }).ToList() ;

            return result;

           
        }



        public async Task<TaskResponseDto> GetTaskById(int id)
        {
            var task = await _context.Tasks.Include(t => t.AssignedUsers).Include(t => t.CreatedBy).FirstOrDefaultAsync(t => t.Id == id);

            var result = new TaskResponseDto
            {
                TaskId = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = task.CreatedAt,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                CloseDate = task.ClosedDate,
                CreatedByUserName = task.CreatedBy.UserName,
                AssignedUsers = task.AssignedUsers.Select(tu => new AssignedUserDto
                {
                    UserId = tu.User.Id,
                    UserName = tu.User.UserName,
                    UserEmail = tu.User.Email,
                    UserClosedDate = tu.ClosedDate,
                    UserStatusInTask = tu.Status.ToString(),
                    AssignedDate = tu.AssignedDate,
                    Feedback = tu.Feedback


                }).ToList()
            };

            return result;
        }

      
    }
}
