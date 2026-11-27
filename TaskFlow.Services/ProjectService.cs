using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Models;
using TaskFlow.Data.Models.Enums;
using TaskFlow.Data.Repository.Interfaces;
using TaskFlow.Services.Interfaces;
using TaskFlow.Web.ViewModels.Projects;

namespace TaskFlow.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    ITeamRepository teamRepository) : IProjectService
{
    public async Task<ProjectIndexViewModel?> GetTeamProjectsAsync(
        int teamId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        var teamAccess = await teamRepository
            .GetAllAttached()
            .AsNoTracking()
            .Where(team => team.Id == teamId
                           && team.Members.Any(member => member.UserId == userId))
            .Select(team => new
            {
                team.Name,
                CanCreateProjects = team.Members
                    .Where(member => member.UserId == userId)
                    .Select(member => member.Role)
                    .Any(role => role == TeamRole.Owner || role == TeamRole.Manager)
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (teamAccess is null)
        {
            return null;
        }

        ProjectListItemViewModel[] projects = await projectRepository
            .GetAllAttached()
            .AsNoTracking()
            .Where(project => project.TeamId == teamId)
            .OrderBy(project => project.Name)
            .Select(project => new ProjectListItemViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Deadline = project.Deadline,
                OwnerName = project.Owner.FirstName + " " + project.Owner.LastName,
                TaskCount = project.Tasks.Count
            })
            .ToArrayAsync(cancellationToken);

        return new ProjectIndexViewModel
        {
            TeamId = teamId,
            TeamName = teamAccess.Name,
            CanCreateProjects = teamAccess.CanCreateProjects,
            Projects = projects
        };
    }

    public async Task<bool> CanManageProjectsAsync(
        int teamId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        return await teamRepository
            .GetAllAttached()
            .AsNoTracking()
            .AnyAsync(team => team.Id == teamId
                              && team.Members.Any(member => member.UserId == userId
                                                            && (member.Role == TeamRole.Owner
                                                                || member.Role == TeamRole.Manager)),
                cancellationToken);
    }

    public async Task<int?> CreateProjectAsync(
        int teamId,
        string ownerId,
        ProjectCreateInputModel inputModel,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerId);
        ArgumentNullException.ThrowIfNull(inputModel);
        ArgumentException.ThrowIfNullOrWhiteSpace(inputModel.Name);

        Team? team = await teamRepository
            .GetAllAttached()
            .Include(candidate => candidate.Members)
            .SingleOrDefaultAsync(candidate => candidate.Id == teamId
                && candidate.Members.Any(member => member.UserId == ownerId
                    && (member.Role == TeamRole.Owner || member.Role == TeamRole.Manager)),
                cancellationToken);

        if (team is null)
        {
            return null;
        }

        var project = new Project
        {
            Name = inputModel.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(inputModel.Description)
                ? null
                : inputModel.Description.Trim(),
            Deadline = inputModel.Deadline,
            TeamId = team.Id,
            OwnerId = ownerId,
            Members = team.Members
                .Select(member => new ProjectMember { UserId = member.UserId })
                .ToArray()
        };

        await projectRepository.AddAsync(project, cancellationToken);
        await projectRepository.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
