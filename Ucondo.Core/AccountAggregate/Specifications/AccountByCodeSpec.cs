using Ardalis.Specification;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class AccountByCodeSpec : SingleResultSpecification<Account>
{
	public AccountByCodeSpec(AccountCode code) => Query.Where(a => a.Code == code);
}