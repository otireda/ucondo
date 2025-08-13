using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.Specifications;
using Ucondo.UseCases.Dtos;

namespace Ucondo.UseCases.Accounts.Get;

public class GetAccountHandler (IReadRepository<Account>  repository) : IQueryHandler<GetAccountQuery, Result<AccountDto>>
{
	public async Task<Result<AccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
	{
		var spec = new AccountByCodeSpecWithParent(request.AccountCode);
		var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);
		if (entity == null) return Result.NotFound();

		return new AccountDto(entity.Code, entity.Name, entity.AllowsPostings, entity.Type, entity.ParentCode);
	}
}