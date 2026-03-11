using Application.Common;
using Application.Contracts.Interfaces;
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
    public class TaskUserRepository : GenericRepository<TaskUser>, ITaskUserRepository
    {
        private readonly ApplicationDbContext _context;
       

        public TaskUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
           
            
        }
        

        public async Task<List<TaskUser>> GetMytasksAsync(int id)
        {
            var userId = id;

            return await _context.TaskUsers.Where(x => x.UserId == userId)
                .Include(x => x.Task).ThenInclude(t => t.CreatedBy)
                .Include(x => x.User).ToListAsync();
        }

        

        public async Task<TaskUser>  GetTaskById(int id)
        {
            var task =await _context.TaskUsers.Include(x => x.Task).ThenInclude(t => t.CreatedBy).Include(x => x.User).FirstOrDefaultAsync(t => t.TaskId == id); ;
            return task;
        }
    }
}
