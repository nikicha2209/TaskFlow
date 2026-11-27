namespace TaskFlow.Web.ViewModels.Teams;

public class TeamIndexViewModel
{
    public IReadOnlyCollection<TeamListItemViewModel> Teams { get; set; }
        = Array.Empty<TeamListItemViewModel>();
}
