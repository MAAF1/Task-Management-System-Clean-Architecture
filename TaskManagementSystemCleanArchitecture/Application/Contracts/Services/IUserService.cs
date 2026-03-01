using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllUsersAsync();

        public Task<UserDto> GetByIdAsync(int id);
    }
}
