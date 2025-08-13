using Ucondo.Core.Enums;

namespace Ucondo.Web.Accounts;

public record AccountRecords(
	int Id,
	string Code,
	string Name,
	bool AllowsPostings,
	AccountType Type,
	int? ParentId);