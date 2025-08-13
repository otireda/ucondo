using Ardalis.Result;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Enums;

namespace Ucondo.UseCases.Accounts.Create;

public record CreateAccountCommand(
	string Name,
	bool AllowsPostings,
	AccountType Type,
	string? ParentCode)
	: Ardalis.SharedKernel.ICommand<Result<string>>;