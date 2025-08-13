using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.UseCases.Dtos;

namespace Ucondo.UseCases.Accounts.List;

public class ListAccountsHandler(IListAccountsQueryService query)
	: IQueryHandler<ListAccountsQuery, Result<IEnumerable<AccountDto>>>
{
	public async Task<Result<IEnumerable<AccountDto>>> Handle(ListAccountsQuery request,
		CancellationToken cancellationToken)
	{
		var result = await query.ListAsync(request.Search, request.PageNumber, request.PageSize);

		return Result.Success(result);
	}
}