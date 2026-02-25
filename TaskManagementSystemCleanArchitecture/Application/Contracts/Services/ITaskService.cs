using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Interfaces;
using Application.DTOs;
namespace Application.Contracts.Services
{
    public interface ITaskService<T>: IGenericRepository<T> where T : class
    {
        Task<IEnumerable<TaskResponseDto>> GetTasksAsync(string? searchTerm = null);
        Task<TaskResponseDto?> GetTaskByIdAsync(int id);
        Task<string> CreateTaskAsync(CreateTaskDto dto, int createdByUserId);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
