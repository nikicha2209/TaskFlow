using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Models;
using TaskFlow.Data.Repository.Interfaces;

namespace TaskFlow.Data.Repository;

public class TeamRepository(ApplicationDbContext dbContext) : ITeamRepository
{
    public IQueryable<Team> GetAllAttached()
    {
        return dbContext.Teams.AsQueryable();
    }

    public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
    {
        await dbContext.Teams.AddAsync(team, cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
