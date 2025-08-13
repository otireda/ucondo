using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.UseCases.Dtos;

namespace Ucondo.UseCases.Accounts.Get;

public record GetAccountQuery(string AccountCode) : IQuery<Result<AccountDto>>;