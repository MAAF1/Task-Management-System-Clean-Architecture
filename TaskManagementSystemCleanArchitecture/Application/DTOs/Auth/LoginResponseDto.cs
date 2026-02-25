using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Message { get; set; } = null!;
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; }
        public string? Username { get; set; }
       
    }
}
