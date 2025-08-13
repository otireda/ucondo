using Ardalis.Specification;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class AccountHasChildrenSpec : Specification<Account>
{
	public AccountHasChildrenSpec(int parentId)
		=> Query.Where(a => a.ParentId == parentId).Take(1);
}