using FastEndpoints;

namespace Ucondo.Web.Accounts.Request;

public class ListAccountsRequest
{
	public const string Route = "/Accounts";

	[QueryParam]
	public string? AccountCode { get; set; }

	[QueryParam]
	public required int PageNumber { get; set; }

	[QueryParam]
	public required int PageSize { get; set; }
}