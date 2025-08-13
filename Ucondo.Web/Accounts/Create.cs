using FastEndpoints;
using MediatR;
using Ucondo.UseCases.Accounts.Create;

namespace Ucondo.Web.Accounts;

public class Create(IMediator mediator) : Endpoint<CreateAccountRequest, CreateAccountResponse>
{
	public override void Configure()
	{
		Post(CreateAccountRequest.Route);
		AllowAnonymous();
	}

	public override async Task HandleAsync(CreateAccountRequest request, CancellationToken cancellationToken) {
		var result = await mediator.Send(new CreateAccountCommand(
			request.Code,
			request.Name,
			request.AllowsPostings,
			request.Type,
			request.ParentId), cancellationToken);

		if (result.IsSuccess)
		{
			Response = new CreateAccountResponse
			{
				ParentId = request.ParentId,
				Code = request.Code,
				Name = request.Name,
				AllowsPostings = request.AllowsPostings,
				Type = request.Type,
			};
			
			return;
		}
	}
}