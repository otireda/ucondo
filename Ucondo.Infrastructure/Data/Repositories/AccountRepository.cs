using Ardalis.Specification.EntityFrameworkCore;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.Specifications;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Interfaces;

namespace Ucondo.Infrastructure.Data.Repositories;

public sealed class AccountRepository(UcondoDbContext db) : RepositoryBase<Account>(db), IAccountRepository
{
	public Task<Account?> GetByCodeAsync(string code, CancellationToken ct = default) =>
		FirstOrDefaultAsync(new AccountByCodeSpec(code), ct);

	public Task<bool> ExistsCodeAsync(string code, CancellationToken ct = default) =>
		AnyAsync(new AccountByCodeSpec(code), ct);

	public Task<bool> HasChildrenAsync(string parentCode, CancellationToken ct = default) =>
		AnyAsync(new AccountHasChildrenSpec(parentCode), ct);

	public Task<Account?> GetMaxDirectChildAsync(string parentCode, CancellationToken ct = default) =>
		FirstOrDefaultAsync(new MaxDirectChildByParentCodeSpec(parentCode), ct);

	public async Task<IReadOnlyList<Account>> GetDirectChildrenAsync(string parentCode, CancellationToken ct = default) => 
		await ListAsync(new DirectChildrenByParentCodeSpec(parentCode), ct);
}