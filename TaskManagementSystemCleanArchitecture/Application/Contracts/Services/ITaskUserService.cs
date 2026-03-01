using Application.DTOs;
using Application.DTOs.UserTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface ITaskUserService
    {
        public Task<UserTaskDetailsDto> GetMyTasksAsync(string? searchTerm = null);
        Task<bool> CompleteTaskAsync(int id);
        Task<bool> UncompleteTaskAsync(int id);
        Task<bool> WriteFeedbackAsync(int id, FeedbackDto dto);
    }
}
