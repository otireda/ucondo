using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.AccountAggregate.Specifications;

public sealed class DirectChildrenByParentCodeSpec : Specification<Account>
{
	public DirectChildrenByParentCodeSpec(string parent)
	{
		var p = parent.ToString();
		var targetDepth = p.Count(ch => ch == '.') + 2;

		Query.AsNoTracking()
			.Where(a => EF.Property<string>(a, nameof(Account.Code)).StartsWith(p + "."))
			.Where(a => (EF.Property<string>(a, nameof(Account.Code)).Length
				- EF.Property<string>(a, nameof(Account.Code)).Replace(".", "").Length + 1) == targetDepth)
			.OrderBy(a => EF.Property<string>(a, nameof(Account.Code)));
	}
}