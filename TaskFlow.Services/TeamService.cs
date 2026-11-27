using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Models;
using TaskFlow.Data.Models.Enums;
using TaskFlow.Data.Repository.Interfaces;
using TaskFlow.Services.Interfaces;
using TaskFlow.Web.ViewModels.Teams;

namespace TaskFlow.Services;

public class TeamService(ITeamRepository teamRepository) : ITeamService
{
    public async Task<TeamIndexViewModel> GetMyTeamsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        TeamListItemViewModel[] teams = await teamRepository
            .GetAllAttached()
            .AsNoTracking()
            .Where(team => team.Members.Any(member => member.UserId == userId))
            .OrderBy(team => team.Name)
            .Select(team => new TeamListItemViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                Role = team.Members
                    .Where(member => member.UserId == userId)
                    .Select(member => member.Role)
                    .FirstOrDefault(),
                MemberCount = team.Members.Count,
                CreatedOn = team.CreatedOn
            })
            .ToArrayAsync(cancellationToken);

        return new TeamIndexViewModel { Teams = teams };
    }

    public async Task<int> CreateTeamAsync(
        TeamCreateInputModel inputModel,
        string ownerId,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(inputModel);
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(inputModel.Name);

        string? description = string.IsNullOrWhiteSpace(inputModel.Description)
            ? null
            : inputModel.Description.Trim();

        var team = new Team
        {
            Name = inputModel.Name.Trim(),
            Description = description,
            OwnerId = ownerId,
            Members =
            [
                new TeamMember
                {
                    UserId = ownerId,
                    Role = TeamRole.Owner
                }
            ]
        };

        await teamRepository.AddAsync(team, cancellationToken);
        await teamRepository.SaveChangesAsync(cancellationToken);

        return team.Id;
    }
}
