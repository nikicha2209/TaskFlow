using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Enums;

namespace TaskFlow.Data.Models;

public class TeamMember
{
    [ForeignKey(nameof(Team))]
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public TeamRole Role { get; set; } = TeamRole.Member;

    public DateTime JoinedOn { get; set; } = DateTime.UtcNow;
}
