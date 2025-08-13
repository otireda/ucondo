using Ardalis.Result;
using Ardalis.SharedKernel;
using Ucondo.UseCases.Dtos;

namespace Ucondo.UseCases.Accounts.Get;

public record GetAccountCodeQuery(string? ParentCode) : IQuery<Result<string>>;