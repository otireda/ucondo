using Ucondo.Core.Enums;

namespace Ucondo.Web.Accounts.Request;

public class CreateAccountRequest
{
	public const string Route = "/Accounts";
	
	public required string Name { get; set; }
	public required bool AllowsPostings { get; set; }
	public required AccountType Type { get; set; }
	public string? ParentCode { get; set; }
}