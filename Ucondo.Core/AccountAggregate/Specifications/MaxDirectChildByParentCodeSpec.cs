using Ardalis.Specification;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class MaxDirectChildByParentCodeSpec : SingleResultSpecification<Account>
{
	public MaxDirectChildByParentCodeSpec(AccountCode parent)
	{
		var depth = parent.Segments.Count + 1;
		Query.Where(a => a.Code.StartsWith(parent) && a.Code.Segments.Count == depth)
			.OrderByDescending(a => a.Code) // relies on IComparable on AccountCode
			.Take(1);
	}
}