using System.ComponentModel.DataAnnotations;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Web.ViewModels.Teams;

public class TeamCreateInputModel
{
    [Required]
    [StringLength(ValidationConstants.Team.NameMaxLength)]
    [Display(Name = "Team name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(ValidationConstants.Team.DescriptionMaxLength)]
    public string? Description { get; set; }
}
