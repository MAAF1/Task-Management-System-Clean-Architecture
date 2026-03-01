using Application.Contracts.Interfaces;
using Application.Contracts.Services;
using Application.DTOs;
using Azure;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _uow.GenericRepository<ApplicationUser>().GetAllAsync();
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                result.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email
                });
            }

            return result;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _uow.GenericRepository<ApplicationUser>().GetByIdAsync(id);

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email

            };

            return result;
        }
    }
}
