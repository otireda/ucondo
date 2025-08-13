using Ucondo.UseCases.Dtos;

namespace Ucondo.Web.Accounts.Response;

public class AccountListResponse
{
	public List<AccountDto> Accounts { get; set; } = [];
}