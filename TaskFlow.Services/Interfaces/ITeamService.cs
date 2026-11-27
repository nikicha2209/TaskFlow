using TaskFlow.Web.ViewModels.Teams;

namespace TaskFlow.Services.Interfaces;

public interface ITeamService
{
    Task<TeamIndexViewModel> GetMyTeamsAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task<int> CreateTeamAsync(
        TeamCreateInputModel inputModel,
        string ownerId,
        CancellationToken cancellationToken = default);
}
