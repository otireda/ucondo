using Microsoft.EntityFrameworkCore;
using Ucondo.UseCases.Accounts.List;
using Ucondo.UseCases.Dtos;

namespace Ucondo.Infrastructure.Data.Queries;

public class ListAccountsQueryService(UcondoDbContext db) : IListAccountsQueryService
{
	public async Task<IEnumerable<AccountDto>> ListAsync(string accountName, int pageNumber, int pageSize)
	{
		var searchParam = $"%{accountName}%";
		var offset = (pageNumber - 1) * pageSize;

		var result = await db.Database
			.SqlQuery<AccountDto>(
				$@"SELECT Code, AllowsPostings, Name, ParentCode, Type, Depth 
               FROM Accounts
               WHERE Name LIKE {searchParam}
               ORDER BY Name
               LIMIT {pageSize} OFFSET {offset}")
			.ToListAsync();

		return result;
	}
}