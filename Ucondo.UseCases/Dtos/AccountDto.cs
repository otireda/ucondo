using Ucondo.Core.Enums;

namespace Ucondo.UseCases.Dtos;

public record AccountDto(string Code, string Name, bool AllowsPostings, AccountType Type, string? ParentCode);