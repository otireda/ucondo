using Ardalis.Specification;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.Interfaces;

public interface IAccountRepository : IRepositoryBase<Account>
{
	Task<Account?> GetByCodeAsync(string code, CancellationToken ct = default);
	Task<bool> ExistsCodeAsync(string code, CancellationToken ct = default);
	Task<bool> HasChildrenAsync(string parentCode, CancellationToken ct = default);
	Task<Account?> GetMaxDirectChildAsync(string parentCode, CancellationToken ct = default);
	Task<IReadOnlyList<Account>> GetDirectChildrenAsync(string parentCode, CancellationToken ct = default);
}