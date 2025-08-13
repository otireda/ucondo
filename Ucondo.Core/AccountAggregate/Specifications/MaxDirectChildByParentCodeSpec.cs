using Ardalis.Specification;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class MaxDirectChildByParentCodeSpec : SingleResultSpecification<Account>
{
	public MaxDirectChildByParentCodeSpec(string parentCode)
	{
		var depth = AccountCode.Parse(parentCode).Segments.Count + 1;
		Query.Where(a => a.Code.StartsWith(parentCode) && a.Depth == depth )
			.OrderByDescending(a => a.Code) 
			.Take(1);
	}
}