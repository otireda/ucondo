using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Ucondo.UseCases.Accounts.Get;
using Ucondo.UseCases.Dtos;

namespace Ucondo.Web.Accounts;

public class GetByCode(IMediator mediator) : Endpoint<GetAccountByCodeRequest, AccountDto>
{
	public override void Configure()
	{
		Get(GetAccountByCodeRequest.Route);
		AllowAnonymous();
	}

	public override async Task HandleAsync(GetAccountByCodeRequest request,
		CancellationToken cancellationToken)
	{
		var query = new GetAccountQuery(request.AccountCode);

		var result = await mediator.Send(query, cancellationToken);

		if (result.Status == ResultStatus.NotFound)
		{
			await Send.NotFoundAsync(cancellationToken);
			return;
		}

		if (result.IsSuccess)
		{
			Response = result.Value;
		}
	}
}