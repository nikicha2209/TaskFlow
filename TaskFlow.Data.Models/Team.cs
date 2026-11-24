using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Data.Models;

public class Team
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.Team.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [StringLength(ValidationConstants.Team.DescriptionMaxLength)]
    public string? Description { get; set; }

    [Required]
    [ForeignKey(nameof(Owner))]
    public string OwnerId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public ICollection<TeamMember> Members { get; set; } = new HashSet<TeamMember>();

    public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
}
