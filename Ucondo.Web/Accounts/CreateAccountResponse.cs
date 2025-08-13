using Ucondo.Core.Enums;

namespace Ucondo.Web.Accounts;

public class CreateAccountResponse
{
	public string? Code { get; set; }
	public string? Name { get; set; }
	public bool? AllowsPostings { get; set; }
	public AccountType? Type { get; set; }
	public int? ParentId { get; set; }
}