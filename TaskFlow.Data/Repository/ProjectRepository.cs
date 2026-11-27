using TaskFlow.Data.Models;
using TaskFlow.Data.Repository.Interfaces;

namespace TaskFlow.Data.Repository;

public class ProjectRepository(ApplicationDbContext dbContext) : IProjectRepository
{
    public IQueryable<Project> GetAllAttached()
    {
        return dbContext.Projects;
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await dbContext.Projects.AddAsync(project, cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
