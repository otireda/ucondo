using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.Specifications;
using Ucondo.Core.Interfaces;
using Ucondo.UseCases.Dtos;

namespace Ucondo.UseCases.Accounts.Get;

public class GetAccountCodeHandler (IAccountCodeSuggester  accountCodeSuggester) : IQueryHandler<GetAccountCodeQuery, Result<string>>
{
	public async Task<Result<string>> Handle(GetAccountCodeQuery request, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(request.ParentCode))
		{
			var code = await accountCodeSuggester.SuggestNextRootAsync(cancellationToken);
			return Result<string>.Success(code);
		}

		var codeChild = await accountCodeSuggester.SuggestNextChildAsync( request.ParentCode, cancellationToken);
		return Result<string>.Success(codeChild);
	}
}