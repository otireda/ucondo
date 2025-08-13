using Ucondo.UseCases.Dtos;

namespace Ucondo.Web.Accounts;

public class AccountListResponse
{
	public List<AccountDto> Accounts { get; set; } = [];
}