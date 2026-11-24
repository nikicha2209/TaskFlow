using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Data.Models;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.Project.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [StringLength(ValidationConstants.Project.DescriptionMaxLength)]
    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    [ForeignKey(nameof(Team))]
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Owner))]
    public string OwnerId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedOn { get; set; }

    public ICollection<ProjectMember> Members { get; set; } = new HashSet<ProjectMember>();

    public ICollection<TaskItem> Tasks { get; set; } = new HashSet<TaskItem>();
}
