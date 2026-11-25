using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Models;

namespace TaskFlow.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Team> Teams => Set<Team>();

    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public DbSet<Subtask> Subtasks => Set<Subtask>();

    public DbSet<Comment> Comments => Set<Comment>();

    public DbSet<Notification> Notifications => Set<Notification>();

    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TeamMember>()
            .HasKey(member => new { member.TeamId, member.UserId });

        builder.Entity<TeamMember>()
            .HasOne(member => member.Team)
            .WithMany(team => team.Members)
            .HasForeignKey(member => member.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TeamMember>()
            .HasOne(member => member.User)
            .WithMany(user => user.TeamMemberships)
            .HasForeignKey(member => member.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Team>()
            .HasOne(team => team.Owner)
            .WithMany(user => user.OwnedTeams)
            .HasForeignKey(team => team.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Team>()
            .HasIndex(team => team.Name);

        builder.Entity<Project>()
            .HasOne(project => project.Team)
            .WithMany(team => team.Projects)
            .HasForeignKey(project => project.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Project>()
            .HasOne(project => project.Owner)
            .WithMany(user => user.OwnedProjects)
            .HasForeignKey(project => project.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Project>()
            .HasIndex(project => new { project.TeamId, project.Name });

        builder.Entity<ProjectMember>()
            .HasKey(member => new { member.ProjectId, member.UserId });

        builder.Entity<ProjectMember>()
            .HasOne(member => member.Project)
            .WithMany(project => project.Members)
            .HasForeignKey(member => member.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProjectMember>()
            .HasOne(member => member.User)
            .WithMany(user => user.ProjectMemberships)
            .HasForeignKey(member => member.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TaskItem>()
            .HasOne(task => task.Project)
            .WithMany(project => project.Tasks)
            .HasForeignKey(task => task.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TaskItem>()
            .HasOne(task => task.Assignee)
            .WithMany(user => user.AssignedTasks)
            .HasForeignKey(task => task.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<TaskItem>()
            .HasOne(task => task.CreatedBy)
            .WithMany(user => user.CreatedTasks)
            .HasForeignKey(task => task.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TaskItem>()
            .HasIndex(task => new { task.ProjectId, task.Status, task.DueDate });

        builder.Entity<Subtask>()
            .HasOne(subtask => subtask.TaskItem)
            .WithMany(task => task.Subtasks)
            .HasForeignKey(subtask => subtask.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>()
            .HasOne(comment => comment.TaskItem)
            .WithMany(task => task.Comments)
            .HasForeignKey(comment => comment.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>()
            .HasOne(comment => comment.Author)
            .WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasIndex(comment => new { comment.TaskItemId, comment.CreatedOn });

        builder.Entity<Notification>()
            .HasOne(notification => notification.User)
            .WithMany(user => user.Notifications)
            .HasForeignKey(notification => notification.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Notification>()
            .HasIndex(notification => new { notification.UserId, notification.IsRead, notification.CreatedOn });

        builder.Entity<ActivityLog>()
            .HasOne(activity => activity.User)
            .WithMany(user => user.ActivityEntries)
            .HasForeignKey(activity => activity.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ActivityLog>()
            .HasOne(activity => activity.Project)
            .WithMany()
            .HasForeignKey(activity => activity.ProjectId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<ActivityLog>()
            .HasIndex(activity => new { activity.ProjectId, activity.CreatedOn });
    }
}
