using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;

namespace Application.Contracts.Services
{
    public interface IJwtProvider
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
