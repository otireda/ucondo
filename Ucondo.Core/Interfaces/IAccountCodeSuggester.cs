using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.Interfaces;

public interface IAccountCodeSuggester
{
	Task<string> SuggestNextChildAsync(string parentCode, CancellationToken ct);

	Task<string> SuggestNextRootAsync(CancellationToken ct);
}