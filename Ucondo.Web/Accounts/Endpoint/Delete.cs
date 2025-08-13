using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Ucondo.UseCases.Accounts.Delete;
using Ucondo.Web.Accounts.Request;

namespace Ucondo.Web.Accounts.Endpoint;

public class Delete(IMediator mediator) : Endpoint<DeleteAccountRequest>
{
	public override void Configure()
	{
		Delete(DeleteAccountRequest.Route);
		AllowAnonymous();
	}
	
	public override async Task HandleAsync(
		DeleteAccountRequest request,
		CancellationToken cancellationToken)
	{
		var command = new DeleteAccountCommand(request.AccountCode);

		var result = await mediator.Send(command, cancellationToken);

		if (result.Status == ResultStatus.NotFound)
		{
			await Send.NotFoundAsync(cancellationToken);
			return;
		}

		if (result.IsSuccess) await Send.NoContentAsync(cancellationToken);
	}
}