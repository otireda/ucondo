using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Core.Interfaces;

public interface IAccountCodeSuggester
{
	Task<AccountCode> SuggestNextChildAsync(AccountCode parentCode, CancellationToken ct);
}