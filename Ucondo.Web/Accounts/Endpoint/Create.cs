using FastEndpoints;
using MediatR;
using Ucondo.UseCases.Accounts.Create;
using Ucondo.Web.Accounts.Request;
using Ucondo.Web.Accounts.Response;

namespace Ucondo.Web.Accounts.Endpoint;

public class Create(IMediator mediator) : Endpoint<CreateAccountRequest, CreateAccountResponse>
{
	public override void Configure()
	{
		Post(CreateAccountRequest.Route);
		AllowAnonymous();
	}

	public override async Task HandleAsync(CreateAccountRequest request, CancellationToken cancellationToken) {
		var result = await mediator.Send(new CreateAccountCommand(
			request.Name,
			request.AllowsPostings,
			request.Type,
			request.ParentCode), cancellationToken);

		if (result.IsSuccess)
		{
			Response = new CreateAccountResponse
			{
				ParentId = request.ParentCode,
				Name = request.Name,
				AllowsPostings = request.AllowsPostings,
				Type = request.Type,
			};
		}
	}
}