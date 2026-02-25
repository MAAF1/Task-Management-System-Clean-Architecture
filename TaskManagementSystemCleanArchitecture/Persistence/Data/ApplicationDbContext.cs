using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
         IdentityUserClaim<int>,
         IdentityUserRole<int>,
         IdentityUserLogin<int>,
         IdentityRoleClaim<int>,
         IdentityUserToken<int>> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TaskEntity>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.Title).IsRequired().HasMaxLength(100);
                e.HasOne(t => t.CreatedBy).WithMany().HasForeignKey(t => t.CreatedById).OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<TaskUser>(e =>
            {   
                e.HasKey(tu => new { tu.TaskId, tu.UserId });
                e.HasOne(tu => tu.Task).WithMany(t => t.AssignedUsers).HasForeignKey(tu => tu.TaskId).OnDelete(DeleteBehavior.Cascade);

                e.HasOne(tu => tu.User).WithMany().HasForeignKey(tu => tu.UserId).OnDelete(DeleteBehavior.Cascade);
            });
        }
            

    }
}
