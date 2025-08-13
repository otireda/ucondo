using System.Reflection;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Ucondo.Core.AccountAggregate;

namespace Ucondo.Infrastructure.Data;

public class UcondoDbContext(
	DbContextOptions<UcondoDbContext> options,
	IDomainEventDispatcher? dispatcher)
	: DbContext(options)
{
	public DbSet<Account> Contributors => Set<Account>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		// ignore events if no dispatcher provided
		if (dispatcher == null) return result;

		// dispatch events only if save was successful
		var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
			.Select(e => e.Entity)
			.Where(e => e.DomainEvents.Any())
			.ToArray();

		await dispatcher.DispatchAndClearEvents(entitiesWithEvents);

		return result;
	}

	public override int SaveChanges() =>
		SaveChangesAsync().GetAwaiter().GetResult();
}