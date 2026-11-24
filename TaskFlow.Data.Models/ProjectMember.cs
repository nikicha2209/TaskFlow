using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Data.Models;

public class ProjectMember
{
    [ForeignKey(nameof(Project))]
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public DateTime AddedOn { get; set; } = DateTime.UtcNow;
}
