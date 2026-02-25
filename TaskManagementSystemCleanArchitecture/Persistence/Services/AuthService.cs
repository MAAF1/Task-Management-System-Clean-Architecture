using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Services;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AuthService(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtProvider jwtProvider)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _roleManager = roleManager;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new LoginResponseDto { Message = "Invalid email or password" };
            }

            var token = await _jwtProvider.GenerateTokenAsync(user);
            return new LoginResponseDto
            {
                Message = "Login successful",
                IsAuthenticated = true,
                Token = token,
                Username = user.UserName
            };

        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequest request)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponseDto { Message = result.Errors.First().Description };
            }
            if(!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = "User"});
            }
            await _userManager.AddToRoleAsync(user, "User");

            return new RegisterResponseDto
            {
                Succeeded = true,
                Message = "User registered successfully"
            };
        }
    }
}
