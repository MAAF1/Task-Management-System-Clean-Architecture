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

        

        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks.Include(tu => tu.AssignedUsers).Include(c => c.CreatedBy).ToListAsync();

            

            return tasks;

           
        }



        public async Task<TaskEntity> GetTaskById(int id)
        {
            var task = await _context.Tasks.Include(t => t.AssignedUsers).Include(t => t.CreatedBy).FirstOrDefaultAsync(t => t.Id == id);

           

            return task;
        }

      
    }
}
