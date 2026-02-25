using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class ApplicationUser: IdentityUser<int>
    {
        public ICollection<TaskUser> AssignedTasks { get; set; } = new List<TaskUser>();
    }
}
