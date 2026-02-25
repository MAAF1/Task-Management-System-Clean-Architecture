using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Application.Contracts.Services;
using Application.DTOs.Auth;
namespace Application.Features.Auth
{
    public record RegisterCommand(RegisterRequest Request) : IRequest<RegisterResponseDto> ;
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IAuthService _authService;
        public RegisterHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<RegisterResponseDto> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(command.Request);
        }
    }
}
