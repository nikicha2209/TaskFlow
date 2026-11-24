using Microsoft.AspNetCore.Identity;
using TaskFlow.Data.Models.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(ValidationConstants.User.NameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.User.NameMaxLength)]
        public string LastName { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Team> OwnedTeams { get; set; } = new HashSet<Team>();

        public ICollection<TeamMember> TeamMemberships { get; set; } = new HashSet<TeamMember>();

        public ICollection<Project> OwnedProjects { get; set; } = new HashSet<Project>();

        public ICollection<ProjectMember> ProjectMemberships { get; set; } = new HashSet<ProjectMember>();

        public ICollection<TaskItem> AssignedTasks { get; set; } = new HashSet<TaskItem>();

        public ICollection<TaskItem> CreatedTasks { get; set; } = new HashSet<TaskItem>();

        public ICollection<Comment> Comments { get; set; }
            = new HashSet<Comment>();

        public ICollection<Notification> Notifications { get; set; }
            = new HashSet<Notification>();

        public ICollection<ActivityLog> ActivityEntries { get; set; } = new HashSet<ActivityLog>();
    }
}
