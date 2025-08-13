using FastEndpoints;

namespace Ucondo.Web.Accounts.Request;

public class GetAccountCodeRequest
{
	public const string Route = "/Accounts/Code";

	[QueryParam] 
	public string? ParentCode { get; set; }
}