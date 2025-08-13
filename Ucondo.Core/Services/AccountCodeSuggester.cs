using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.Specifications;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Interfaces;

namespace Ucondo.Core.Services;

public class AccountCodeSuggester(IReadRepository<Account> repo) : IAccountCodeSuggester
{
	public async Task<string> SuggestNextChildAsync(string parentCode, CancellationToken ct)
	{
		var parentAccountCode = AccountCode.Parse(parentCode);
		var maxChild = await repo.FirstOrDefaultAsync( new MaxDirectChildByParentCodeSpec(parentCode), ct );

		if (maxChild is null)
			return AccountCode.CreateChild(parentAccountCode, 1);

		var maxChildAccountCode = AccountCode.Parse(maxChild.Code);
		
		var lastSeg = maxChildAccountCode.Segments.Last();
		if (lastSeg < 999)
			return AccountCode.CreateChild(parentAccountCode, lastSeg + 1);

		// carry to an upper parent according to your policy
		var newParent = AccountCode.Parse(parentAccountCode.Segments.First().ToString());

		var maxAtNewParent = await repo.FirstOrDefaultAsync(
			new MaxDirectChildByParentCodeSpec(newParent.ToString()), ct);

		var child = (maxAtNewParent is null) ? 1 : AccountCode.Parse(maxAtNewParent.Code).Segments.Last() + 1;
		if (child > 999) throw new Exception("Espaço de código esgotado no novo pai");

		return AccountCode.CreateChild(newParent, child);
	}

	public async Task<string> SuggestNextRootAsync(CancellationToken ct)
	{
		var maxRoot = await repo.FirstOrDefaultAsync(new MaxRootAccountSpec(), ct);

		if (maxRoot is null)
			return AccountCode.CreateRoot(1);

		var maxAccountRootCode = AccountCode.Parse(maxRoot.Code);
		var n = maxAccountRootCode.Segments[0];
		if (n >= 999)
			throw new Exception("Não há mais códigos raiz disponíveis (máximo 999).");

		return AccountCode.CreateRoot(n + 1);
	}
}