using TaskFlow.Data.Models;

namespace TaskFlow.Data.Repository.Interfaces;

public interface ITeamRepository
{
    IQueryable<Team> GetAllAttached();

    Task AddAsync(Team team, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
