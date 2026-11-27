using System.ComponentModel.DataAnnotations;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Web.ViewModels.Projects;

public class ProjectCreateInputModel
{
    [Required]
    [StringLength(ValidationConstants.Project.NameMaxLength)]
    [Display(Name = "Project name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(ValidationConstants.Project.DescriptionMaxLength)]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Deadline")]
    public DateTime? Deadline { get; set; }
}
