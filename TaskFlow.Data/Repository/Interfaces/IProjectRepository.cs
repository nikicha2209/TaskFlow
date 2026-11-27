using TaskFlow.Data.Models;

namespace TaskFlow.Data.Repository.Interfaces;

public interface IProjectRepository
{
    IQueryable<Project> GetAllAttached();

    Task AddAsync(Project project, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
