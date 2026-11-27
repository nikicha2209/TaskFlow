namespace TaskFlow.Web.ViewModels.Projects;

public class ProjectIndexViewModel
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = string.Empty;

    public bool CanCreateProjects { get; set; }

    public IReadOnlyCollection<ProjectListItemViewModel> Projects { get; set; }
        = Array.Empty<ProjectListItemViewModel>();
}
