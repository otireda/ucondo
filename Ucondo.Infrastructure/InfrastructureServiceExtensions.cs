using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ucondo.Core.Interfaces;
using Ucondo.Core.Services;
using Ucondo.Infrastructure.Data;
using Ucondo.Infrastructure.Data.Repositories;

namespace Ucondo.Infrastructure;

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
		services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
		services.AddScoped(typeof(IAccountCodeSuggester), typeof(AccountCodeSuggester));
		// services.AddScoped<IListContributorsQueryService, ListContributorsQueryService>();
		// services.AddScoped<IDeleteContributorService, DeleteContributorService>();

		return services;
	}
}