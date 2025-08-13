using Ucondo.Core.Enums;

namespace Ucondo.Web.Accounts;

public record AccountRecords(
	string Code,
	string Name,
	bool AllowsPostings,
	AccountType Type,
	string ParentId);