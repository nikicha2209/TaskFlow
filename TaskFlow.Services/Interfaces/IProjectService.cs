using TaskFlow.Web.ViewModels.Projects;

namespace TaskFlow.Services.Interfaces;

public interface IProjectService
{
    Task<ProjectIndexViewModel?> GetTeamProjectsAsync(
        int teamId,
        string userId,
        CancellationToken cancellationToken = default);

    Task<bool> CanManageProjectsAsync(
        int teamId,
        string userId,
        CancellationToken cancellationToken = default);

    Task<int?> CreateProjectAsync(
        int teamId,
        string ownerId,
        ProjectCreateInputModel inputModel,
        CancellationToken cancellationToken = default);
}
