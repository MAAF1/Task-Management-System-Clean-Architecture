using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Services;
using Application.DTOs.Auth;
using MediatR;
namespace Application.Features.Auth
{
    public record LoginCommand(LoginRequest Request) : IRequest<LoginResponseDto>;
    public class LoginHandler: IRequestHandler<LoginCommand,LoginResponseDto>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(command.Request);
        }

    }

}
