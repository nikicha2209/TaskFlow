using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TaskFlow.Web.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Assembly repositoryAssembly)
    {
        RegisterByConvention(services, repositoryAssembly, "Repository");
        return services;
    }

    public static IServiceCollection AddUserDefinedServices(
        this IServiceCollection services,
        Assembly serviceAssembly)
    {
        RegisterByConvention(services, serviceAssembly, "Service");
        return services;
    }

    private static void RegisterByConvention(
        IServiceCollection services,
        Assembly assembly,
        string implementationSuffix)
    {
        IEnumerable<Type> implementations = assembly
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false }
                           && type.Name.EndsWith(implementationSuffix, StringComparison.Ordinal));

        foreach (Type implementation in implementations)
        {
            Type? contract = implementation.GetInterfaces()
                .FirstOrDefault(type => type.Name == $"I{implementation.Name}");

            if (contract is null)
            {
                throw new InvalidOperationException(
                    $"The implementation '{implementation.FullName}' must implement 'I{implementation.Name}'.");
            }

            services.AddScoped(contract, implementation);
        }
    }
}
