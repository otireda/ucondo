using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Ucondo.UseCases.Accounts.List;
using Ucondo.UseCases.Dtos;
using Ucondo.Web.Accounts.Request;
using Ucondo.Web.Accounts.Response;

namespace Ucondo.Web.Accounts.Endpoint;

public class List(IMediator mediator) : Endpoint<ListAccountsRequest, AccountListResponse>
{
	public override void Configure()
	{
		Get(ListAccountsRequest.Route);
		AllowAnonymous();
	}

	public override async Task HandleAsync(ListAccountsRequest request, CancellationToken cancellationToken)
	{
		Result<IEnumerable<AccountDto>> result = await mediator.Send(new ListAccountsQuery(request.AccountCode, request.PageSize, request.PageNumber), cancellationToken);

		if (result.IsSuccess)
		{
			Response = new AccountListResponse()
			{
				Accounts = result.Value.ToList()
			};
		}
	}
}