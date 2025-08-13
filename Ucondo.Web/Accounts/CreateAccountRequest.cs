using Ucondo.Core.Enums;

namespace Ucondo.Web.Accounts;

public class CreateAccountRequest
{
	public const string Route = "/Accounts";
	
	public required string Code { get; set; }
	public required string Name { get; set; }
	public required bool AllowsPostings { get; set; }
	public required AccountType Type { get; set; }
	public int? ParentId { get; set; }
}