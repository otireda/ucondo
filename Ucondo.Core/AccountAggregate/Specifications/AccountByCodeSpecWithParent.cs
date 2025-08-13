using Ardalis.Specification;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class AccountByCodeSpecWithParent : SingleResultSpecification<Account>
{
	public AccountByCodeSpecWithParent(string code) => Query.Include( x => x.Parent ).Where(a => a.Code == code);
}