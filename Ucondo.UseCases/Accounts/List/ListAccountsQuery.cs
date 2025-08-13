using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.UseCases.Dtos;

namespace Ucondo.UseCases.Accounts.List;

public record ListAccountsQuery(string Search, int PageSize, int PageNumber) : IQuery<Result<IEnumerable<AccountDto>>>;