using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AssignedUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;

       
        public string UserStatusInTask { get; set; } = null!; 
        public string? Feedback { get; set; }
        public DateTime? AssignedDate { get; set; }
    }
}
