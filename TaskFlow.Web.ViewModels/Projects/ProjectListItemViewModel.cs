namespace TaskFlow.Web.ViewModels.Projects;

public class ProjectListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public string OwnerName { get; set; } = string.Empty;

    public int TaskCount { get; set; }
}
