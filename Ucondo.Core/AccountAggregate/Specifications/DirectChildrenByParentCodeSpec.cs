using Ardalis.Specification;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class DirectChildrenByParentCodeSpec : Specification<Account>
{
	public DirectChildrenByParentCodeSpec(AccountCode parent)
	{
		var depth = parent.Segments.Count + 1;
		Query.Where(a => a.Code.StartsWith(parent) && a.Code.Segments.Count == depth);
	}
}