namespace Ucondo.Web.Accounts.Request;

public class GetAccountByCodeRequest
{
	public const string Route = "/Accounts/{AccountCode}";

	public required string AccountCode { get; set; }
}