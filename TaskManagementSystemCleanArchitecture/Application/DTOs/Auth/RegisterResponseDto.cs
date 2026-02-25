using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = null!;
        public IEnumerable<string>? Errors { get; set; }
    }
}
