namespace Ucondo.Web.Accounts;

public class ListAccountsRequest
{
	public const string Route = "/Accounts/{AccountCode}/{PageNumber}/{PageSize}";

	public required string AccountCode { get; set; }
	public required int PageNumber { get; set; }
	public required int PageSize { get; set; }
}