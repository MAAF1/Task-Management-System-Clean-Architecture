using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskResponseDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Status { get; set; } = null!; 
        public string CreatedByUserName { get; set; } = null!; 
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }

        // قائمة باليوزرز اللي شغالين على التاسك وتفاصيلهم فيها
        public List<AssignedUserDto> AssignedUsers { get; set; } = new();
    }
}
