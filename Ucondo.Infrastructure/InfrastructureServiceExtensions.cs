using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ucondo.Core.Interfaces;
using Ucondo.Core.Services;
using Ucondo.Infrastructure.Data;
using Ucondo.Infrastructure.Data.Queries;
using Ucondo.Infrastructure.Data.Repositories;
using Ucondo.UseCases.Accounts.List;

namespace Ucondo.Infrastructure;

public static class InfrastructureServiceExtensions
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		ConfigurationManager config)
	{
		var connectionString = config.GetConnectionString("SqliteConnection");
		Guard.Against.Null(connectionString);
		services.AddDbContext<UcondoDbContext>(options =>
			options.UseSqlite(connectionString));

		services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
		services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
		services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
		services.AddScoped(typeof(IAccountCodeSuggester), typeof(AccountCodeSuggester));
		services.AddScoped<IListAccountsQueryService, ListAccountsQueryService>();

		return services;
	}
}