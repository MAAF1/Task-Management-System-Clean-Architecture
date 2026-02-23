using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class TaskUser
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string? Feedback { get; set; }
        public Status Status { get; set; }
        public DateTime? AssignedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public TaskEntity Task { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
