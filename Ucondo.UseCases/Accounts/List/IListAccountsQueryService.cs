using Ucondo.UseCases.Dtos;

namespace Ucondo.UseCases.Accounts.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListAccountsQueryService
{
	Task<IEnumerable<AccountDto>> ListAsync(string accountName, int pageNumber, int pageSize);
}