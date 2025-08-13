using Ardalis.Result;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Enums;

namespace Ucondo.UseCases.Accounts.Create;

public record CreateAccountCommand(
	string Code,
	string Name,
	bool AllowsPostings,
	AccountType Type,
	int? Parent)
	: Ardalis.SharedKernel.ICommand<Result<int>>;