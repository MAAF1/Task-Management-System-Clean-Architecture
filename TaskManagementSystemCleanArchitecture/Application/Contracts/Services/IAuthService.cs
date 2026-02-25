using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Auth;

namespace Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequest request);
        Task<LoginResponseDto> LoginAsync(LoginRequest request);
    }
}
