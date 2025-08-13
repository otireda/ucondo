namespace Ucondo.Web.Accounts.Request;

public class DeleteAccountRequest
{
	public const string Route = "/Accounts/{AccountCode}";
	
	public required string AccountCode { get; set; }
}