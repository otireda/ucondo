using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Ucondo.UseCases.Accounts.Delete;

public record DeleteAccountCommand(string AccountCode) : ICommand<Result>;