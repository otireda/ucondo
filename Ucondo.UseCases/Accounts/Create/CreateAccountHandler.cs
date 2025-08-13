using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Interfaces;

namespace Ucondo.UseCases.Accounts.Create;

public class CreateAccountHandler(IAccountRepository accountRepository, IAccountCodeSuggester accountCodeSuggester) : ICommandHandler<CreateAccountCommand, Result<string>>
{
	public async Task<Result<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(request.ParentCode))
			return await CreateRoot(request, cancellationToken);
		
		return await CreateChildren( request, cancellationToken );
	}

	private async Task<string> CreateRoot(CreateAccountCommand request, CancellationToken cancellationToken)
	{
		var accountCode = await accountCodeSuggester.SuggestNextRootAsync(cancellationToken);
		var codeIsUnique = await accountRepository.ExistsCodeAsync(accountCode, cancellationToken);

		var newAccountRoot = Account.Create(AccountCode.Parse(accountCode).Depth, accountCode, request.Name, request.AllowsPostings, request.Type, null,
			true,
			!codeIsUnique, true,
			true);

		var createdItem = await accountRepository.AddAsync(newAccountRoot, cancellationToken);
		return createdItem.Code;
	}

	private async Task<string> CreateChildren(CreateAccountCommand request, CancellationToken cancellationToken)
	{
		var accountParent =
			await accountRepository.GetByCodeAsync(request.ParentCode!, cancellationToken);
	
		if (accountParent == null) throw new Exception("Conta pai não encontrada.");

		var accountCode = await accountCodeSuggester.SuggestNextChildAsync(accountParent.Code, cancellationToken);
		var codeIsUnique = await accountRepository.ExistsCodeAsync(accountCode, cancellationToken);

		var newAccount = Account.Create(AccountCode.Parse(accountCode).Depth, accountCode, request.Name, request.AllowsPostings, request.Type, accountParent,
			!accountParent.AllowsPostings,
			!codeIsUnique, accountParent.Type == request.Type,
			true);

		var createdItem = await accountRepository.AddAsync(newAccount, cancellationToken);

		return createdItem.Code;
	}
}