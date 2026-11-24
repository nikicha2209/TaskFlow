using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Data.Models;

public class Subtask
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.Subtask.TitleMaxLength)]
    public string Title { get; set; } = string.Empty;

    [StringLength(ValidationConstants.Subtask.DescriptionMaxLength)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    [ForeignKey(nameof(TaskItem))]
    public int TaskItemId { get; set; }

    public TaskItem TaskItem { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
