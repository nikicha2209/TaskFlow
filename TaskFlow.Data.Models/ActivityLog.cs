using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Data.Models;

public class ActivityLog
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.ActivityLog.ActionMaxLength)]
    public string Action { get; set; } = string.Empty;

    [StringLength(ValidationConstants.ActivityLog.DetailsMaxLength)]
    public string? Details { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    [ForeignKey(nameof(Project))]
    public int? ProjectId { get; set; }

    public Project? Project { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
