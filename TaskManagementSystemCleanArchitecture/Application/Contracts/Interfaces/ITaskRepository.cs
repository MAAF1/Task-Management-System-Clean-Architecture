using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Interfaces
{
    public interface ITaskRepository : IGenericRepository<TaskEntity>
    {
        Task<List<TaskEntity>> GetAllTasksAsync();
        Task<TaskEntity> GetTaskById(int id);
    }
}
