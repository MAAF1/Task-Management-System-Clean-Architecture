using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

       
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public Status Status { get; set; }

        public int CreatedById { get; set; } // FK to User
        public ApplicationUser CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }

       


        public ICollection<TaskUser> AssignedUsers { get; set; } = new List<TaskUser>();
    }
}
