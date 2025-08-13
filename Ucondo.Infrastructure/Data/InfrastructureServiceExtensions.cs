using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ucondo.Infrastructure.Data;

public static class InfrastructureServiceExtensions
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		ConfigurationManager config)
	{
		string? connectionString = config.GetConnectionString("SqliteConnection");
		Guard.Against.Null(connectionString);
		services.AddDbContext<UcondoDbContext>(options =>
			options.UseSqlite(connectionString));

		services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
		services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
		// services.AddScoped<IListContributorsQueryService, ListContributorsQueryService>();
		// services.AddScoped<IDeleteContributorService, DeleteContributorService>();

		return services;
	}
}