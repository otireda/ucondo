using Ardalis.Specification;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class AccountHasChildrenSpec : Specification<Account>
{
	public AccountHasChildrenSpec(string code)
		=> Query.Where(a => a.ParentCode! == code).Take(1);
}