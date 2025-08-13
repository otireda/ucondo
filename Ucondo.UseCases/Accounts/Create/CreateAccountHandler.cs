using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate;

namespace Ucondo.UseCases.Accounts.Create;

public class CreateAccountHandler(IRepository<Account> _repository) : ICommandHandler<CreateAccountCommand, Result<int>>
{
	public async Task<Result<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
	{
		// var newAccount = new Account(request.Code, request.Name, request.AllowsPosting, request.Type, request.ParentId);
		// if (!string.IsNullOrEmpty(request.PhoneNumber))
		// {
		// 	newContributor.SetPhoneNumber(request.PhoneNumber);
		// }
		//
		// var createdItem = await _repository.AddAsync(newContributor, cancellationToken);

		// return createdItem.Id;

		return 0;
	}
}