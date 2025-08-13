using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.Core.Interfaces;

namespace Ucondo.UseCases.Accounts.Delete;

public class DeleteAccountHandler(IAccountRepository accountRepository)
	: ICommandHandler<DeleteAccountCommand, Result>
{
	public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
	{
		var account = await accountRepository.GetByCodeAsync( request.AccountCode );
		if (account == null) return Result.NotFound("Conta não encontrada");

		await accountRepository.DeleteAsync(account, cancellationToken);
		return Result.Success();
	}
}