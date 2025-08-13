using Ardalis.Specification;

namespace Ucondo.Core.AccountAggregate.Specifications;

public class MaxRootAccountSpec : SingleResultSpecification<Account>
{
	public MaxRootAccountSpec()
	{
		Query.Where(a => a.ParentCode == null)
			.OrderByDescending(a => a.Code)
			.AsNoTracking()
			.Take(1);
	}
}