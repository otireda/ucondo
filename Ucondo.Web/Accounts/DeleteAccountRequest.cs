namespace Ucondo.Web.Accounts;

public class DeleteAccountRequest
{
	public const string Route = "/Accounts/{AccountCode:string}";

	public static string BuildRoute(string accountCode) =>
		Route.Replace("{AccountCode:int}", accountCode);

	public required string AccountCode { get; set; }
}