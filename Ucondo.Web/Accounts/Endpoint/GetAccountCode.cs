using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Ucondo.Core.Interfaces;
using Ucondo.Core.Services;
using Ucondo.UseCases.Accounts.Get;
using Ucondo.UseCases.Dtos;
using Ucondo.Web.Accounts.Request;

namespace Ucondo.Web.Accounts.Endpoint;

public class GetAccountCode(IMediator mediator) : Endpoint<GetAccountCodeRequest, string>
{
	public override void Configure()
	{
		Get(GetAccountCodeRequest.Route);
		AllowAnonymous();
	}

	public override async Task HandleAsync(GetAccountCodeRequest request, CancellationToken cancellationToken)
	{
		var query = new GetAccountCodeQuery(request.ParentCode);

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