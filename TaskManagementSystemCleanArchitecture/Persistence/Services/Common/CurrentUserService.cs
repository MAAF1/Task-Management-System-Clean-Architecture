using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Application.Common;

namespace Persistence.Services.Common
{
    public class CurrentUserService:ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string? UserId => _contextAccessor.HttpContext?.User?.FindFirstValue("uid");

        public string? Role => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    }
}
