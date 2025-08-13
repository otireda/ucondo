using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.Specifications;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Interfaces;

namespace Ucondo.Core.Services;

public class AccountCodeSuggester(IReadRepository<Account> repo) : IAccountCodeSuggester
{
	public async Task<AccountCode> SuggestNextChildAsync(AccountCode parentCode, CancellationToken ct)
	{
		var maxChild = await repo.FirstOrDefaultAsync(
			new MaxDirectChildByParentCodeSpec(parentCode), ct);

		if (maxChild is null)
			return AccountCode.CreateChild(parentCode, 1);

		var lastSeg = maxChild.Code.Segments.Last();
		if (lastSeg < 999)
			return AccountCode.CreateChild(parentCode, lastSeg + 1);

		// carry to an upper parent according to your policy
		var newParent = AccountCode.Parse(parentCode.Segments.First().ToString());

		var maxAtNewParent = await repo.FirstOrDefaultAsync(
			new MaxDirectChildByParentCodeSpec(newParent), ct);

		var child = (maxAtNewParent is null) ? 1 : maxAtNewParent.Code.Segments.Last() + 1;
		if (child > 999) throw new Exception("Espaço de código esgotado no novo pai");

		return AccountCode.CreateChild(newParent, child);
	}
}