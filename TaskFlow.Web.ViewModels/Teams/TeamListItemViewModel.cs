using TaskFlow.Data.Models.Enums;

namespace TaskFlow.Web.ViewModels.Teams;

public class TeamListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TeamRole Role { get; set; }

    public int MemberCount { get; set; }

    public DateTime CreatedOn { get; set; }
}
