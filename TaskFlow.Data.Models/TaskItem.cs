using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Common.Constants;
using TaskFlow.Data.Models.Enums;

namespace TaskFlow.Data.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.TaskItem.TitleMaxLength)]
    public string Title { get; set; } = string.Empty;

    [StringLength(ValidationConstants.TaskItem.DescriptionMaxLength)]
    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; } = TaskItemStatus.ToDo;

    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public DateTime? DueDate { get; set; }

    [ForeignKey(nameof(Project))]
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    [ForeignKey(nameof(Assignee))]
    public string? AssigneeId { get; set; }

    public ApplicationUser? Assignee { get; set; }

    [Required]
    [ForeignKey(nameof(CreatedBy))]
    public string CreatedById { get; set; } = string.Empty;

    public ApplicationUser CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedOn { get; set; }

    public ICollection<Subtask> Subtasks { get; set; } = new HashSet<Subtask>();

    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
}
