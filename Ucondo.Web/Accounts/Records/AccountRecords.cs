using Ucondo.Core.Enums;

namespace Ucondo.Web.Accounts.Records;

public record AccountRecords(
	string Code,
	string Name,
	bool AllowsPostings,
	AccountType Type,
	string ParentId);